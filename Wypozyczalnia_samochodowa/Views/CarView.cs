using System;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Views
{
    public class CarView
    {
        private readonly CarController _carController;

        public CarView(CarController carController)
        {
            _carController = carController;
        }

        public string ShowManageCarsMenu()
        {
            AnsiConsole.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Manage Cars")
                    .AddChoices(new[] { "List Cars", "Add Car", "Update Car", "Delete Car", "Return to Main Menu" })
            );
        }

        public void DisplayAvailableCars(List<Car> cars)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Available Cars:[/]");
            if (cars == null || cars.Count == 0)
            {
                 AnsiConsole.MarkupLine("[yellow]No cars available.[/]");
                 return;
            }

            foreach (var car in cars)
            {
                 AnsiConsole.MarkupLine($"- ID: {car.Id}, {car.Marka} {car.Model}, Seats: {car.Siedzenia}, Price: {car.Cena} zł");
            }
        }
      

        

        public Car GetNewCarDetails()
        {
            try
            {
                AnsiConsole.Clear();
                var make = AnsiConsole.Ask<string>("Enter car make:");
                var model = AnsiConsole.Ask<string>("Enter car model:");
                var seats = AnsiConsole.Ask<int>("Enter seat count:");
                var price = AnsiConsole.Ask<int>("Enter price:");

                return new Car
                {
                    Marka = make,
                    Model = model,
                    Siedzenia = seats,
                    Dostepnosc = true,
                    Cena = price
                };
            }
            catch
            {
                ShowErrorMessage("Failed to add car. Invalid input.");
                return null;
            }
        }

        public Car GetUpdatedCarDetails(Car car)
        {
            try
            {
                AnsiConsole.Clear();
                car.Marka = AnsiConsole.Ask<string>("Enter new make:", car.Marka);
                car.Model = AnsiConsole.Ask<string>("Enter new model:", car.Model);
                car.Siedzenia = AnsiConsole.Ask<int>("Enter new seat count:", car.Siedzenia);
                car.Cena = AnsiConsole.Ask<int>("Enter new price:", car.Cena);
                return car;
            }
            catch
            {
                ShowErrorMessage("Failed to update car. Invalid input.");
                return null;
            }
        }

        public int GetCarId(string action)
        {
            try
            {
                return AnsiConsole.Ask<int>($"Enter car ID to {action}:");
            }
            catch
            {
                ShowErrorMessage("Invalid car ID.");
                return -1;
            }
        }

        public bool ConfirmDeletion(Car car)
        {
            return AnsiConsole.Confirm($"Are you sure you want to delete {car.Marka} {car.Model}?");
        }

        public void ShowSuccessMessage(string message)
        {
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }

        public void ShowErrorMessage(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }

        public void ShowWarningMessage(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }
    }
}
