using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserReservationWindow : Window
    {
        private ReservationController _reservationController;
        private Customer _currentUser;

        public UserReservationWindow(ReservationController reservationController, Customer currentUser)
        {
            InitializeComponent();
            _reservationController = reservationController;
            _currentUser = currentUser;
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            //_reservationController.ReserveCarForUser(_currentUser);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
