using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class DeleteCarWindow : Window
    {
        private CarController _carController;

        public DeleteCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            _carController.DeleteCar();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
