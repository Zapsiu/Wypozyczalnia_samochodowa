using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class DeleteCarWindow : Window
    {
        private readonly CarController _carController;

        public DeleteCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CarIdTextBox.Text, out int carId))
            {
                // Użycie istniejącej metody DeleteCar
                _carController.DeleteCar();
                MessageBox.Show("Samochód został usunięty!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawny ID samochodu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
