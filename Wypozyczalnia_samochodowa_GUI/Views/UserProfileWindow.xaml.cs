using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserProfileWindow : Window
    {
        private CustomerController _customerController;
        private Customer _currentUser;

        public UserProfileWindow(CustomerController customerController, Customer currentUser)
        {
            InitializeComponent();
            _customerController = customerController;
            _currentUser = currentUser;
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            _customerController.UpdateProfile(_currentUser);
            MessageBox.Show("Profile updated successfully!");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
