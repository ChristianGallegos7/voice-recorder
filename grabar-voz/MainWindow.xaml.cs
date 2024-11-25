using System;
using System.Linq;
using System.Windows;
using NAudio.Wave;

namespace grabar_voz
{
    public partial class MainWindow : Window
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;

        public MainWindow()
        {
            InitializeComponent();
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
                MessageBox.Show("Grabación iniciada");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar grabación: {ex.Message}");
            }
        }

        private void StopRecording_Click(object sender, RoutedEventArgs e)
        {
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