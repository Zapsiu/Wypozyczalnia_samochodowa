using System.Windows;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Data;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class AdminUserListWindow : Window
    {
        private readonly CustomerController _customerController;

        public AdminUserListWindow()
        {
            InitializeComponent();
            _customerController = new CustomerController(new RentalDbContext());
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _customerController.GetAllUsers();
            UserListView.ItemsSource = users;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
