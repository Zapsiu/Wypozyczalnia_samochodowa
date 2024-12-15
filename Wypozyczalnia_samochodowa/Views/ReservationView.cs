using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Controllers;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Views
{
    public class ReservationView
    {
        private readonly ReservationController _reservationController;

        public ReservationView(ReservationController reservationController)
        {
            _reservationController = reservationController;
        }

        public List<Car> FilterAndSortCars(List<Car> cars)
        {
            while (true)
            {
                if (!cars.Any())
                {
                    ShowMessage("No cars available.", "yellow");
                    return new List<Car>();
                }

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Filter and sort options:")
                        .AddChoices(new[] { "Show all", "Filter by brand", "Filter by seats", "Sort by price (ascending)", "Sort by price (descending)", "Filter by price range", "Return" })
                );

                if (action == "Return")
                {
                    return new List<Car>();
                }

                if (action == "Filter by brand")
                {
                    var brand = AnsiConsole.Ask<string>("Enter the brand to filter by:");
                    cars = cars.Where(c => c.Marka.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (!cars.Any())
                    {
                        ShowMessage($"No cars found for brand '{brand}'.", "yellow");
                        continue;
                    }
                }
                else if (action == "Filter by seats")
                {
                    var seats = AnsiConsole.Ask<int>("Enter the number of seats to filter by:");
                    cars = cars.Where(c => c.Siedzenia == seats).ToList();

                    if (!cars.Any())
                    {
                        ShowMessage($"No cars found with {seats} seats.", "yellow");
                        continue;
                    }
                }
                else if (action == "Sort by price (ascending)")
                {
                    cars = cars.OrderBy(c => c.Cena).ToList();
                }
                else if (action == "Sort by price (descending)")
                {
                    cars = cars.OrderByDescending(c => c.Cena).ToList();
                }
                else if (action == "Filter by price range")
                {
                    var minPrice = AnsiConsole.Ask<int>("Enter the minimum price:");
                    var maxPrice = AnsiConsole.Ask<int>("Enter the maximum price:");
                    cars = cars.Where(c => c.Cena >= minPrice && c.Cena <= maxPrice).ToList();

                    if (!cars.Any())
                    {
                        ShowMessage($"No cars found in the price range {minPrice} zł - {maxPrice} zł.", "yellow");
                        continue;
                    }
                }

                return cars;
            }
        }

        public Car SelectCarToReserve(List<Car> availableCars)
        {
            var options = availableCars
                .Select(c => $"{c.Id}. {c.Marka} {c.Model}, Seats: {c.Siedzenia}, Price: {c.Cena} zł")
                .ToList();
            options.Add("Cancel");

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a car to reserve or choose 'Cancel' to go back")
                    .AddChoices(options)
            );

            if (selectedOption == "Cancel") return null;

            int carId = int.Parse(selectedOption.Split('.')[0]);
            return availableCars.SingleOrDefault(c => c.Id == carId);
        }

        public void DisplayReservations(List<Reservation> reservations)
        {
            while (true)
            {
                AnsiConsole.Clear();

                if (!reservations.Any())
                {
                    ShowMessage("No reservations found.", "yellow");
                    AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[grey]Press [green]Enter[/] to return to the menu[/]")
                            .AddChoices("Return")
                    );
                    return;
                }

                var filteredReservations = reservations;

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices(new[] { "Show all", "Filter by brand", "Filter by seats", "Filter by date", "Return" })
                );

                if (action == "Return")
                {
                    return;
                }

                if (action == "Filter by brand")
                {
                    var brand = AnsiConsole.Ask<string>("Enter the brand to filter by:");
                    filteredReservations = reservations
                        .Where(r => r.Car != null && r.Car.Marka.Equals(brand, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found for brand '{brand}'.", "yellow");
                        continue;
                    }
                }
                else if (action == "Filter by seats")
                {
                    var seats = AnsiConsole.Ask<int>("Enter the number of seats to filter by:");
                    filteredReservations = reservations
                        .Where(r => r.Car != null && r.Car.Siedzenia == seats)
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found for cars with {seats} seats.", "yellow");
                        continue;
                    }
                }
                else if (action == "Filter by date")
                {
                    var startDate = AnsiConsole.Ask<DateTime>("Enter the start date (yyyy-MM-dd):");
                    var endDate = AnsiConsole.Ask<DateTime>("Enter the end date (yyyy-MM-dd):");
                    filteredReservations = reservations
                        .Where(r => r.Poczatek >= startDate && r.Koniec <= endDate)
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}.", "yellow");
                        continue;
                    }
                }

                var table = new Table()
                    .AddColumn("ID")
                    .AddColumn("Car")
                    .AddColumn("Registration")
                    .AddColumn("Customer")
                    .AddColumn("Start Date")
                    .AddColumn("End Date");

                foreach (var reservation in filteredReservations)
                {
                    var car = reservation.Car ?? new Car { Marka = "N/A", Model = "N/A" };
                    var customer = reservation.Customer ?? new Customer { Imie = "N/A", Nazwisko = "N/A" };

                    table.AddRow(
                        reservation.Id.ToString(),
                        $"{car.Marka} {car.Model}",
                        car.NumerRejestracyjny ?? "N/A",
                        $"{customer.Imie} {customer.Nazwisko}",
                        reservation.Poczatek.ToShortDateString(),
                        reservation.Koniec.ToShortDateString()
                    );
                }

                AnsiConsole.Write(
                    new Panel(table)
                        .Header("Reservations", Justify.Center)
                        .Border(BoxBorder.Rounded)
                );

                AnsiConsole.MarkupLine("[grey]Use arrows to scroll if the list is long.[/]");
                AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[grey]Press [green]Enter[/] to return to the menu[/]")
                        .AddChoices("Return")
                );
            }
        }

        public Reservation SelectReservationToDelete(List<Reservation> reservations)
        {
            while (true)
            {
                if (!reservations.Any())
                {
                    ShowMessage("No reservations available for deletion.", "yellow");
                    return null;
                }

                var filteredReservations = reservations;

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Would you like to filter reservations before deleting?")
                        .AddChoices(new[] { "Show all", "Filter by brand", "Filter by seats", "Filter by date", "Return" })
                );

                if (action == "Return")
                {
                    return null;
                }

                if (action == "Filter by brand")
                {
                    var brand = AnsiConsole.Ask<string>("Enter the brand to filter by:");
                    filteredReservations = reservations
                        .Where(r => r.Car != null && r.Car.Marka.Equals(brand, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found for brand '{brand}'.", "yellow");
                        continue;
                    }
                }
                else if (action == "Filter by seats")
                {
                    var seats = AnsiConsole.Ask<int>("Enter the number of seats to filter by:");
                    filteredReservations = reservations
                        .Where(r => r.Car != null && r.Car.Siedzenia == seats)
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found for cars with {seats} seats.", "yellow");
                        continue;
                    }
                }
                else if (action == "Filter by date")
                {
                    var startDate = AnsiConsole.Ask<DateTime>("Enter the start date (yyyy-MM-dd):");
                    var endDate = AnsiConsole.Ask<DateTime>("Enter the end date (yyyy-MM-dd):");
                    filteredReservations = reservations
                        .Where(r => r.Poczatek >= startDate && r.Koniec <= endDate)
                        .ToList();

                    if (!filteredReservations.Any())
                    {
                        ShowMessage($"No reservations found between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}.", "yellow");
                        continue;
                    }
                }

                var options = filteredReservations
                    .Select(r => $"ID: {r.Id}, Car: {r.Car?.Marka ?? "N/A"} {r.Car?.Model ?? "N/A"}, Reserved by: {r.Customer?.Imie ?? "N/A"} {r.Customer?.Nazwisko ?? "N/A"}")
                    .ToList();
                options.Add("Cancel");

                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a reservation to delete or choose 'Cancel' to go back")
                        .AddChoices(options)
                );

                if (selectedOption == "Cancel") return null;

                int reservationId = int.Parse(selectedOption.Split(',')[0].Split(':')[1].Trim());
                return filteredReservations.SingleOrDefault(r => r.Id == reservationId);
            }
        }

        public bool ConfirmDeletion(Reservation reservation)
        {
            return AnsiConsole.Confirm($"Are you sure you want to delete Reservation ID: {reservation.Id}?");
        }

        public (DateTime StartDate, DateTime EndDate)? GetReservationDates()
        {
            try
            {
                var startDate = AnsiConsole.Ask<DateTime>("Enter start date (yyyy-MM-dd):");
                var endDate = AnsiConsole.Ask<DateTime>("Enter end date (yyyy-MM-dd):");
                return (startDate, endDate);
            }
            catch
            {
                ShowMessage("Invalid date format.", "red");
                return null;
            }
        }

        public void ShowMessage(string message, string color)
        {
            AnsiConsole.MarkupLine($"[{color}]{message}[/]");
        }
    }
}
