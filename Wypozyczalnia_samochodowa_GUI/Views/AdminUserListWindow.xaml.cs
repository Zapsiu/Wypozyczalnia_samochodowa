using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminUserListWindow : Window
    {
        private CustomerController _customerController;

        public AdminUserListWindow(CustomerController customerController)
        {
            InitializeComponent();
            _customerController = customerController;
            LoadUsers();
        }

        private void LoadUsers()
        {
            List<Customer> users = _customerController.DisplayUsers();
            UserListBox.ItemsSource = users;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit user functionality is not implemented.");
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem != null)
            {
                var user = (User)UserListBox.SelectedItem;
                _customerController.DeleteUser();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Select a user to delete.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
