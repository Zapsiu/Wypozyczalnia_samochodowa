using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class CarListWindow : Window
    {
        private CarController _carController;

        public CarListWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
            LoadCars();
        }

        private void LoadCars()
        {
            var cars = _carController.DisplayAvailableCars();
            CarListBox.ItemsSource = cars;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
