using System.Linq;
using System.Windows;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa_GUI.Views;

namespace Wypozyczalnia_samochodowa_GUI
{
    public partial class MainWindow : Window
    {
        private readonly RentalDbContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new RentalDbContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = _dbContext.Customers
                .FirstOrDefault(c => c.Username == username && c.Password == password);

            if (user != null)
            {
                if (user.IsAdmin)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
                else
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane logowania!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
