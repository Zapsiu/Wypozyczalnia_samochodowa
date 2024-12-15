using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserProfileWindow : Window
    {
        private readonly CustomerController _customerController;
        private readonly Customer _currentUser;

        public UserProfileWindow(CustomerController customerController, Customer currentUser)
        {
            InitializeComponent();
            _customerController = customerController;
            _currentUser = currentUser; // Przekazanie obiektu bieżącego użytkownika
        }

        private void UpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            // Wywołanie metody UpdateProfile z przekazanym użytkownikiem
            _customerController.UpdateProfile(_currentUser);

            MessageBox.Show("Profil zaktualizowany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
