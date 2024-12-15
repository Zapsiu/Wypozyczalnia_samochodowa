using System.Collections.Generic;
using System.Windows;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa_GUI.Views
{
    public partial class UserCarListWindow : Window
    {
        public UserCarListWindow(List<Car> cars)
        {
            InitializeComponent();
            CarListView.ItemsSource = cars;
        }
    }
}
