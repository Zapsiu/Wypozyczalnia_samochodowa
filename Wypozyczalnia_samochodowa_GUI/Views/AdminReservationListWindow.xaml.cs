using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminReservationListWindow : Window
    {
        private ReservationController _reservationController;

        public AdminReservationListWindow(ReservationController reservationController)
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

        // Event handler for the "Back" button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Closes the window
        }
    }
}
