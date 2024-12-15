using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Data;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminReservationListWindow : Window
    {
        private readonly ReservationController _reservationController;

        public AdminReservationListWindow()
        {
            InitializeComponent();
            _reservationController = new ReservationController(new RentalDbContext());
            LoadReservations();
        }

        private void LoadReservations()
        {
            var reservations = _reservationController.GetAllReservations();
            ReservationListView.ItemsSource = reservations;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
