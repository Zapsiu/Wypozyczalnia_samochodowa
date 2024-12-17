using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class ReservationWindow : Window
    {
        private ReservationController _reservationController;
        private CustomerController _customerController;

        public ReservationWindow(ReservationController reservationController, CustomerController customerController)
        {
            InitializeComponent();
            _reservationController = reservationController;
            _customerController = customerController;
            LoadCars();
        }

        private void LoadCars()
        {
            var cars = _reservationController.GetAllReservations(); // Przykład metody
            CarComboBox.ItemsSource = cars;
        }

        private void OnReserveClick(object sender, RoutedEventArgs e)
        {
            var selectedCar = CarComboBox.SelectedItem;
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            if (selectedCar == null || startDate == null || endDate == null)
            {
                MessageBox.Show("Please fill in all fields to proceed.");
                return;
            }

            _reservationController.ReserveCarForUser(selectedCar, startDate.Value, _customerController);
            MessageBox.Show("Reservation successful!");
            this.Close();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
