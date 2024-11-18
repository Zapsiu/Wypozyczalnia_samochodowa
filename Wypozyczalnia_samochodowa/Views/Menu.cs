using Spectre.Console;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Views
{
    public static class Menu
    {
        public static Customer ShowInitialMenu(CustomerController customerController)
        {
            while (true)
            {
                AnsiConsole.Clear();
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("==== Car Rental Application ====")
                        .AddChoices(new[] { "Log In", "Sign Up", "Exit" })
                );

                switch (selectedOption)
                {
                    case "Log In":
                        var user = customerController.LogIn();
                        if (user != null) return user;
                        break;
                    case "Sign Up":
                        var newUser = customerController.SignUp();
                        if (newUser != null) return newUser;
                        break;
                    case "Exit":
                        return null;
                }
            }
        }

        public static int ShowUserMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("==== Car Rental Application ====")
                        .AddChoices(new[] { "List available cars", "Make a reservation", "Update profile", "Show reservations", "Exit" })
                );

                switch (selectedOption)
                {
                    case "List available cars":
                        return 1;
                    case "Make a reservation":
                        return 2;
                    case "Update profile":
                        return 3;
                    case "Show reservations":
                        return 4;
                    case "Exit":
                        return 5;
                }
            }
        }

        public static int ShowAdminMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("==== Car Rental Application (Admin) ====")
                        .AddChoices(new[]
                        {
                            "List available cars",
                            "Make a reservation",
                            "Update profile",
                            "Show reservations",
                            "Delete reservation", 
                            "Manage customers",
                            "Manage cars",
                            "Exit"
                        })
                );

                switch (selectedOption)
                {
                    case "List available cars":
                        return 1;
                    case "Make a reservation":
                        return 2;
                    case "Update profile":
                        return 3;
                    case "Show reservations":
                        return 4;
                    case "Delete reservation":
                        return 5;
                    case "Manage customers":
                        return 6;
                    case "Manage cars":
                        return 7;
                    case "Exit":
                        return 8;
                }
            }
        }
    }
}
