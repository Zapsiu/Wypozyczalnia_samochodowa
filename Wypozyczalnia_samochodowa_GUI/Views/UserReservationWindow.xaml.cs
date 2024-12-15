using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserReservationWindow : Window
    {
        private readonly Customer _user;
        private readonly CarController _carController;
        private readonly ReservationController _reservationController;

        public UserReservationWindow(Customer user, CarController carController, ReservationController reservationController)
        {
            InitializeComponent();
            _user = user;
            _carController = carController;
            _reservationController = reservationController;
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CarIdTextBox.Text, out int carId))
            {
                var result = _reservationController.ReserveCarForUserById(_user, carId);
                MessageBox.Show(result ? "Rezerwacja zakończona sukcesem!" : "Nie udało się zarezerwować samochodu.");
            }
            else
            {
                MessageBox.Show("Podaj poprawne ID samochodu.");
            }
        }
    }
}
