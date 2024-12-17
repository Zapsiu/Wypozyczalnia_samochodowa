using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AddCarWindow : Window
    {
        private CarController _carController;

        public AddCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementation for adding a car
            _carController.AddCar();
            MessageBox.Show("Car added successfully!");
        }

        // Event handler for the "Back" button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Closes the current window
        }
    }
}
