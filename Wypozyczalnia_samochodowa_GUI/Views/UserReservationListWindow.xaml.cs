using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserReservationListWindow : Window
    {
        private ReservationController _reservationController;

        public UserReservationListWindow(ReservationController reservationController)
        {
            InitializeComponent();
            _reservationController = reservationController;
            LoadReservations();
        }

        private void LoadReservations()
        {
            var reservations = _reservationController.GetAllReservations();
            ReservationListBox.ItemsSource = reservations;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
