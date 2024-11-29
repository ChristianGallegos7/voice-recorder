using System.Windows;

namespace grabar_voz
{
    public partial class ClientInfoWindow : Window
    {
        public string ClientId { get; private set; }
        public string ClientName { get; private set; }
        public string Observation { get; private set; }

        public ClientInfoWindow()
        {
            InitializeComponent();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            ClientId = IdTextBox.Text;
            ClientName = NameTextBox.Text;
            Observation = ObservationTextBox.Text;

            if (string.IsNullOrWhiteSpace(ClientId) || string.IsNullOrWhiteSpace(ClientName))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true; // Indica que se aceptó
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indica que se canceló
            Close();
        }
    }
}
