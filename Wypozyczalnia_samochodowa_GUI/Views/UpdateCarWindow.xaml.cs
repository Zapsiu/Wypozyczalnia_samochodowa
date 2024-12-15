using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UpdateCarWindow : Window
    {
        private readonly CarController _carController;

        public UpdateCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void UpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CarIdTextBox.Text, out int carId))
            {
                _carController.UpdateCar();
                MessageBox.Show("Samochód zaktualizowany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne ID samochodu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
