using System.Windows;
using System.Windows.Threading;
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

        public MainWindow()
        {
            InitializeComponent();
            ConfigurarTimer();
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
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            TimerTextBlock.Text = elapsedTime.ToString(@"mm\:ss");
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer?.Write(e.Buffer, 0, e.BytesRecorded);
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose();
            waveIn?.Dispose();
            MessageBox.Show($"Grabación guardada en: {outputFilePath}");
        }

        private void StartRecording_Click(object sender, RoutedEventArgs e)
        {
            // Validar dispositivos de entrada de audio
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

                // Reiniciar y arrancar el temporizador
                elapsedTime = TimeSpan.Zero;
                TimerTextBlock.Text = "00:00";
                timer.Start();
                MessageBox.Show("Grabación iniciada");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar grabación: {ex.Message}");
            }
        }

        private void StopRecording_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop(); // Detener el temporizador
            waveIn?.StopRecording();
        }

        private bool HayMicrofonosDisponibles()
        {
            // Verificar si hay dispositivos de entrada de audio
            return WaveInEvent.DeviceCount > 0;
        }

        // Método adicional para mostrar dispositivos de audio disponibles
        private void MostrarDispositivosDeAudio()
        {
            for (int i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                var capabilities = WaveInEvent.GetCapabilities(i);
                MessageBox.Show($"Dispositivo {i}: {capabilities.ProductName}");
            }
        }
    }
}