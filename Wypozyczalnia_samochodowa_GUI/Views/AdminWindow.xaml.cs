using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminWindow : Window
    {
        private CarController _carController;
        private CustomerController _customerController;
        private ReservationController _reservationController;

        public AdminWindow(CarController carController, CustomerController customerController, ReservationController reservationController)
        {
            InitializeComponent();
            _carController = carController;
            _customerController = customerController;
            _reservationController = reservationController;
        }

        private void OnListCarsClick(object sender, RoutedEventArgs e)
        {
            var carListWindow = new CarListWindow(_carController);
            carListWindow.ShowDialog();
        }

        private void OnMakeReservationClick(object sender, RoutedEventArgs e)
        {
            var reservationWindow = new ReservationWindow(_reservationController, _customerController);
            reservationWindow.ShowDialog();
        }

        private void OnUpdateProfileClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Admin profile update not implemented.");
        }

        private void OnShowReservationsClick(object sender, RoutedEventArgs e)
        {
            var reservationListWindow = new AdminReservationListWindow(_reservationController);
            reservationListWindow.ShowDialog();
        }

        private void OnDeleteReservationClick(object sender, RoutedEventArgs e)
        {
            _reservationController.DeleteReservation();
            MessageBox.Show("Reservation deleted successfully.");
        }

        private void OnManageCustomersClick(object sender, RoutedEventArgs e)
        {
            var userListWindow = new AdminUserListWindow(_customerController);
            userListWindow.ShowDialog();
        }

        private void OnManageCarsClick(object sender, RoutedEventArgs e)
        {
            var carListWindow = new AddCarWindow(_carController);
            carListWindow.ShowDialog();
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
