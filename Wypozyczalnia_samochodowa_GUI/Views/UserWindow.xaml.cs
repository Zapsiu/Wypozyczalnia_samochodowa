using System.Windows;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void ViewCars_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja przeglądania samochodów jeszcze niezaimplementowana.", "Informacja");
        }

        private void MyReservations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja wyświetlania rezerwacji jeszcze niezaimplementowana.", "Informacja");
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
