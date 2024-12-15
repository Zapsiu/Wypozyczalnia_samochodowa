using System;
using System.Diagnostics;
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
            AnsiConsole.Clear();

            // Wybór trybu z użyciem Spectre.Console
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Wybierz tryb uruchomienia aplikacji:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Tryb tekstowy",
                        "Tryb graficzny (GUI)",
                        "Wyjście"
                    }));

            if (mode == "Tryb graficzny (GUI)")
            {
                AnsiConsole.MarkupLine("[green]Uruchamianie trybu graficznego...[/]");
                try
                {
                    // Ścieżka do pliku .exe interfejsu WPF
                    Process.Start(@"C:\Users\barte\source\repos\Wypozyczalnia_samochodowa\Wypozyczalnia_samochodowa_GUI\bin\Debug\net8.0-windows\Wypozyczalnia_samochodowa_GUI.exe");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Błąd uruchamiania interfejsu graficznego:[/] {ex.Message}");
                }
            }
            else if (mode == "Tryb tekstowy")
            {
                RunTextMode();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Kończenie programu...[/]");
            }
        }

        static void RunTextMode()
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
                        carController.HandleListCars();
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
