using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AddCarWindow : Window
    {
        private readonly CarController _carController;

        public AddCarWindow(CarController carController)
        {
            InitializeComponent();
            _carController = carController;
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PriceTextBox.Text, out int price))
            {
                var car = new Car
                {
                    Marka = BrandTextBox.Text,
                    Model = ModelTextBox.Text,
                    NumerRejestracyjny = RegistrationNumberTextBox.Text,
                    Cena = price,
                    Dostepnosc = true
                };

                _carController.AddCar(car);
                MessageBox.Show("Samochód został dodany!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Podaj poprawną cenę!");
            }
        }
    }
}
