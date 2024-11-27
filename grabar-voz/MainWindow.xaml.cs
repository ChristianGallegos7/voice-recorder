using System.IO;
using System.Windows;
using System.Windows.Threading;
using Azure.Storage.Blobs;
using NAudio.Wave;

namespace grabar_voz
{
    public partial class MainWindow : Window
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;

        private DispatcherTimer timer; // Temporizador para el tiempo transcurrido
        private TimeSpan elapsedTime;  // Tiempo acumulado

        private bool isRecording = false;
        private bool isPaused = false;
        private bool isStopped = false;  // Flag para saber si se ha detenido la grabación

        public MainWindow()
        {
            InitializeComponent();
            ConfigurarTimer();

            // Establecer estados iniciales
            isRecording = false;
            isPaused = false;
            isStopped = false;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            if (!isRecording && !isPaused) // Estado inicial o después de detener
            {
                StartRecording.IsEnabled = true;  // Habilitado para iniciar grabación
                PauseRecording.IsEnabled = false; // No se puede pausar
                StopRecording.IsEnabled = false;  // No se puede detener
            }
            else if (isRecording && !isPaused) // Grabando
            {
                StartRecording.IsEnabled = false; // No se puede iniciar mientras graba
                PauseRecording.IsEnabled = true;  // Habilitado para pausar
                StopRecording.IsEnabled = true;  // Habilitado para detener
            }
            else if (isPaused) // Pausado
            {
                StartRecording.IsEnabled = true;  // Habilitado para reanudar
                PauseRecording.IsEnabled = false; // No se puede pausar mientras está pausado
                StopRecording.IsEnabled = true;  // Habilitado para detener
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Obtener el área de trabajo (excluyendo la barra de tareas)
            var workArea = SystemParameters.WorkArea;

            // Establecer la posición de la ventana en la esquina inferior derecha
            this.Left = workArea.Right - this.Width; // Derecha de la pantalla
            this.Top = workArea.Bottom - this.Height; // Abajo de la pantalla
        }

        private void ConfigurarTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Actualización cada segundo
            };
            timer.Tick += Timer_Tick;
            elapsedTime = TimeSpan.Zero;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isRecording || isPaused)
            {
                elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
                TimerTextBlock.Text = elapsedTime.ToString(@"mm\:ss");
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer?.Write(e.Buffer, 0, e.BytesRecorded);
        }

        private async void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                if (isStopped) // Solo guardar si se ha detenido la grabación
                {
                    writer?.Dispose();
                    waveIn?.Dispose();
                    outputFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Grabacion_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
                    MessageBox.Show($"Guardando archivo en: {outputFilePath}");


                    // Subir a Azure
                    await SubirArchivoAzure(outputFilePath);
                    isStopped = false; // Resetear la bandera
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en WaveIn_RecordingStopped: {ex.Message}");
            }
        }


        private void PauseRecording_Click(object sender, RoutedEventArgs e)
        {
            if (isRecording && !isPaused) // Si está grabando y no pausado
            {
                waveIn.StopRecording(); // Pausar grabación
                timer.Stop();           // Detener el temporizador
                isPaused = true;
            }
            else if (isPaused) // Si está pausado
            {
                waveIn.StartRecording(); // Reanudar grabación
                timer.Start();           // Reanudar el temporizador
                isPaused = false;
            }
            UpdateButtonStates(); // Actualizar estados de botones
        }

        private void StartRecording_Click(object sender, RoutedEventArgs e)
        {
            if (!HayMicrofonosDisponibles())
            {
                MessageBox.Show("No se detectaron micrófonos. Conecte un micrófono e intente nuevamente.",
                    "Error de dispositivo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1 kHz, mono
                outputFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Grabacion_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.DataAvailable += WaveIn_DataAvailable;
                waveIn.RecordingStopped += WaveIn_RecordingStopped;
                waveIn.StartRecording();

                if (!isPaused) // Solo reiniciar el tiempo si no se está reanudando
                {
                    elapsedTime = TimeSpan.Zero;
                    TimerTextBlock.Text = "00:00";
                }
                timer.Start();

                isRecording = true;
                isPaused = false;
                isStopped = false;
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar grabación: {ex.Message}");
            }
        }

        private async void StopRecording_Click(object sender, RoutedEventArgs e)
        {
            if (isRecording || isPaused) // Si está grabando o pausado
            {
                waveIn.StopRecording(); // Detener grabación
                timer.Stop();           // Detener el temporizador

                isRecording = false;
                isPaused = false;
                isStopped = true; // Marcar como detenido
                UpdateButtonStates();

                // Asegurarse de que la grabación se haya detenido
                MessageBox.Show("Grabación detenida. Guardando...");

                // Guardar y subir el archivo
                writer?.Dispose();
                waveIn?.Dispose();
                TimerTextBlock.Text = "00:00";
                await SubirArchivoAzure(outputFilePath);
            }
        }


        private bool HayMicrofonosDisponibles()
        {
            // Verificar si hay dispositivos de entrada de audio
            return WaveInEvent.DeviceCount > 0;
        }

        public async Task SubirArchivoAzure(string rutaArchivo)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=mi-cuenta;AccountKey=mi-clave;EndpointSuffix=core.windows.net";
            string containerName = "grabaciones";

            try
            {
                //crear cliente de blob
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                //crear el contenedor
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                await containerClient.CreateIfNotExistsAsync();

                // Nombre del blob
                string blobName = Path.GetFileName(rutaArchivo);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                //Subir archivos
                using (FileStream uploadFileStream = File.OpenRead(rutaArchivo))
                {
                    await blobClient.UploadAsync(uploadFileStream, overwrite: true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al subir archivo a Azure: {ex.Message}");
            }
        }
    }
}
