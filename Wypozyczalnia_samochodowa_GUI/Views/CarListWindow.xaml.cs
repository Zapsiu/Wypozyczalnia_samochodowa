using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Data;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class CarListWindow : Window
    {
        private readonly CarController _carController;

        public CarListWindow()
        {
            InitializeComponent();
            _carController = new CarController(new RentalDbContext());
            LoadCars();
        }

        private void LoadCars()
        {
            var cars = _carController.GetAvailableCars();
            CarListView.ItemsSource = cars;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
