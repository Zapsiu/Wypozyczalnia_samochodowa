using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserWindow : Window
    {
        private CarController _carController;
        private CustomerController _customerController;
        private ReservationController _reservationController;
        private Customer _currentUser;

        public UserWindow(CarController carController, CustomerController customerController, ReservationController reservationController)
        {
            InitializeComponent();
            _carController = carController;
            _customerController = customerController;
            _reservationController = reservationController;
        }

        private void ViewCars_Click(object sender, RoutedEventArgs e)
        {
            var carListWindow = new UserCarListWindow(_carController);
            carListWindow.ShowDialog();
        }

        private void MakeReservation_Click(object sender, RoutedEventArgs e)
        {
            var reservationWindow = new UserReservationWindow(_reservationController, _currentUser);
            reservationWindow.ShowDialog();
        }

        private void UpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new UserProfileWindow(_customerController, _currentUser);
            profileWindow.ShowDialog();
        }

        private void ViewReservations_Click(object sender, RoutedEventArgs e)
        {
            var reservationListWindow = new AdminReservationListWindow(_reservationController);
            reservationListWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Implement logout functionality
            this.Close();
        }
    }
}
