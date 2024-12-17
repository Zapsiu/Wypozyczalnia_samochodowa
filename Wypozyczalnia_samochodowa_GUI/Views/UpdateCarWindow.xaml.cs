using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UpdateCarWindow : Window
    {
        private CarController _carController;

        public UpdateCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void UpdateCar_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych z pól tekstowych
            string carIdText = CarIdTextBox.Text;
            string newBrand = BrandTextBox.Text;
            string newModel = ModelTextBox.Text;

            // Walidacja danych
            if (string.IsNullOrEmpty(carIdText) || string.IsNullOrEmpty(newBrand) || string.IsNullOrEmpty(newModel))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(carIdText, out int carId))
            {
                MessageBox.Show("Invalid Car ID. Please enter a number.");
                return;
            }

            // Aktualizacja samochodu za pomocą kontrolera
            _carController.UpdateCar();//carId, newBrand, newModel);
            MessageBox.Show("Car updated successfully!");
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zamknięcie bieżącego okna
        }
    }
}
