using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserReservationListWindow : Window
    {
        private readonly ReservationController _reservationController;
        private readonly int _userId;

        public UserReservationListWindow(ReservationController reservationController, int userId)
        {
            InitializeComponent();
            _reservationController = reservationController;
            _userId = userId;
            LoadReservations();
        }

        private void LoadReservations()
        {
            var reservations = _reservationController.GetReservationsByUser(_userId);
            ReservationListView.ItemsSource = reservations;
        }
    }
}
