using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminWindow : Window
    {
        private readonly CarController _carController;
        private readonly CustomerController _customerController;
        private readonly ReservationController _reservationController;

        public AdminWindow()
        {
            InitializeComponent();

            // Inicjalizacja kontrolerów i kontekstu bazy danych
            var dbContext = new RentalDbContext();
            _carController = new CarController(dbContext);
            _customerController = new CustomerController(dbContext);
            _reservationController = new ReservationController(dbContext);
        }

        private void ListCarsButton_Click(object sender, RoutedEventArgs e)
        {
            _carController.HandleListCars();
        }

        private void MakeReservationButton_Click(object sender, RoutedEventArgs e)
        {
            _reservationController.ReserveCarForUser(null); // Null lub zalogowany użytkownik
        }

        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _customerController.UpdateProfile(null); // Null lub zalogowany użytkownik
        }

        private void ShowReservationsButton_Click(object sender, RoutedEventArgs e)
        {
            _reservationController.DisplayReservations();
        }

        private void DeleteReservationButton_Click(object sender, RoutedEventArgs e)
        {
            _reservationController.DeleteReservation();
        }

        private void ManageCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            _customerController.ManageUsers();
        }

        private void ManageCarsButton_Click(object sender, RoutedEventArgs e)
        {
            _carController.ManageCars();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
