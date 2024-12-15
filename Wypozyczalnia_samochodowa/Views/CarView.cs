using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Views
{
    public class CarView
    {
        public string ShowManageCarsMenu()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Manage Cars")
                    .AddChoices(new[] { "List Cars", "Add Car", "Update Car", "Delete Car", "Return to Main Menu" })
            );
        }

        public string AskListAllOrFiltered()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Would you like to list all cars or filter?")
                    .AddChoices("All", "Filtered")
            );
        }

        public void DisplayAvailableCars(List<Car> cars)
        {
            if (!cars.Any())
            {
                ShowErrorMessage("No cars available.");
                return;
            }

            var table = CreateCarTable(cars);
            AnsiConsole.Write(table);

            PauseAndClear();
        }

        public void DisplayAvailableCarsWithFilters(List<Car> cars)
        {
            while (true)
            {
                if (!cars.Any())
                {
                    ShowErrorMessage("No cars available.");
                    PauseAndClear();
                    return;
                }

                var filteredCars = cars;

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Filter options")
                        .AddChoices(new[] { "Show all", "Filter by brand", "Filter by seats", "Sort by price (ascending)", "Sort by price (descending)", "Filter by price range", "Return" })
                );

                if (action == "Return")
                {
                    PauseAndClear();
                    return;
                }

                if (action == "Filter by brand")
                {
                    var brand = AnsiConsole.Ask<string>("Enter the brand to filter by:");
                    filteredCars = cars.Where(c => c.Marka.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (!filteredCars.Any())
                    {
                        ShowErrorMessage($"No cars found for brand '{brand}'.");
                        continue;
                    }
                }
                else if (action == "Filter by seats")
                {
                    var seats = AnsiConsole.Ask<int>("Enter the number of seats to filter by:");
                    filteredCars = cars.Where(c => c.Siedzenia == seats).ToList();

                    if (!filteredCars.Any())
                    {
                        ShowErrorMessage($"No cars found with {seats} seats.");
                        continue;
                    }
                }
                else if (action == "Sort by price (ascending)")
                {
                    filteredCars = filteredCars.OrderBy(c => c.Cena).ToList();
                }
                else if (action == "Sort by price (descending)")
                {
                    filteredCars = filteredCars.OrderByDescending(c => c.Cena).ToList();
                }
                else if (action == "Filter by price range")
                {
                    var minPrice = AnsiConsole.Ask<int>("Enter the minimum price:");
                    var maxPrice = AnsiConsole.Ask<int>("Enter the maximum price:");
                    filteredCars = cars.Where(c => c.Cena >= minPrice && c.Cena <= maxPrice).ToList();

                    if (!filteredCars.Any())
                    {
                        ShowErrorMessage($"No cars found in the price range {minPrice} zł - {maxPrice} zł.");
                        continue;
                    }
                }

                var table = CreateCarTable(filteredCars);
                AnsiConsole.Write(table);
                PauseAndClear();
            }
        }

        private Table CreateCarTable(List<Car> cars)
        {
            var table = new Table()
                .AddColumn("ID")
                .AddColumn("Make")
                .AddColumn("Model")
                .AddColumn("Seats")
                .AddColumn("Price")
                .AddColumn("Available");

            foreach (var car in cars)
            {
                table.AddRow(
                    car.Id.ToString(),
                    car.Marka,
                    car.Model,
                    car.Siedzenia.ToString(),
                    $"{car.Cena} zł",
                    car.Dostepnosc ? "[green]Yes[/]" : "[red]No[/]"
                );
            }

            return table;
        }

        private void PauseAndClear()
        {
            AnsiConsole.MarkupLine("[grey]Press any key to return...[/]");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        public void ShowErrorMessage(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }

        public void ShowSuccessMessage(string message)
        {
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }

        public void ShowWarningMessage(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }

        public Car GetNewCarDetails()
        {
            var registrationNumber = AnsiConsole.Ask<string>("Enter registration number:");
            var make = AnsiConsole.Ask<string>("Enter car make:");
            var model = AnsiConsole.Ask<string>("Enter car model:");
            var seats = AnsiConsole.Ask<int>("Enter seat count:");
            var price = AnsiConsole.Ask<int>("Enter price:");
            return new Car
            {
                NumerRejestracyjny = registrationNumber,
                Marka = make,
                Model = model,
                Siedzenia = seats,
                Dostepnosc = true,
                Cena = price
            };
        }

        public int GetCarId(string action)
        {
            return AnsiConsole.Ask<int>($"Enter the ID of the car you want to {action}:");
        }

        public Car GetUpdatedCarDetails(Car car)
        {
            car.NumerRejestracyjny = AnsiConsole.Ask<string>("Enter new registration number:", car.NumerRejestracyjny);
            car.Marka = AnsiConsole.Ask<string>("Enter new make:", car.Marka);
            car.Model = AnsiConsole.Ask<string>("Enter new model:", car.Model);
            car.Siedzenia = AnsiConsole.Ask<int>("Enter new seat count:", car.Siedzenia);
            car.Cena = AnsiConsole.Ask<int>("Enter new price:", car.Cena);

            return car;
        }

        public bool ConfirmDeletion(Car car)
        {
            return AnsiConsole.Confirm($"Are you sure you want to delete car {car.Marka} {car.Model} with registration {car.NumerRejestracyjny}?");
        }
    }
}
