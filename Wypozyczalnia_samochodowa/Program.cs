using System;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;
using Wypozyczalnia_samochodowa.Views;

namespace Wypozyczalnia_samochodowa
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new RentalDbContext();
            var carController = new CarController(dbContext);
            var customerController = new CustomerController(dbContext);
            var reservationController = new ReservationController(dbContext);

            Customer currentUser = Menu.ShowInitialMenu(customerController);
            if (currentUser == null)
            {
                AnsiConsole.MarkupLine("[red]Exiting the application...[/]");
                return;
            }

            bool exit = false;
            while (!exit)
            {
                int choice = currentUser.IsAdmin ? Menu.ShowAdminMenu() : Menu.ShowUserMenu();
                AnsiConsole.Clear();
                switch (choice)
                {
                    case 1:
                        carController.ShowAvailableCars();
                        break;
                    case 2:
                        reservationController.ReserveCarForUser(currentUser);
                        break;
                    case 3:
                        customerController.UpdateProfile(currentUser);
                        break;
                    case 4:
                        reservationController.DisplayReservations();
                        break;
                    case 5:
                        if (currentUser.IsAdmin)
                        {
                            reservationController.DeleteReservation();
                        }
                        else
                        {
                            exit = true;
                            AnsiConsole.MarkupLine("[green]Thank you for using the car rental application.[/]");
                        }
                        break;
                    case 6:
                        if (currentUser.IsAdmin)
                        {
                            customerController.ManageUsers();
                        }
                        break;
                    case 7:
                        if (currentUser.IsAdmin)
                        {
                            carController.ManageCars();
                        }
                        break;
                    case 8:
                        exit = true;
                        AnsiConsole.MarkupLine("[green]Thank you for using the car rental application.[/]");
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                        break;
                }
                if (!exit)
                {
                    AnsiConsole.MarkupLine("\n[grey]Press any key to return to the main menu...[/]");
                    Console.ReadKey(true);
                    AnsiConsole.Clear();
                }
            }
        }
    }
}
