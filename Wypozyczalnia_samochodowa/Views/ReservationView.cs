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

        public Car SelectCarToReserve(List<Car> availableCars)
        {
            var carOptions = availableCars
                .Select(c => $"{c.Id}. {c.Marka} {c.Model}, Seats: {c.Siedzenia}, Price: {c.Cena} zł")
                .ToList();
            carOptions.Add("Cancel");

            var selectedCarOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a car to reserve or choose 'Cancel' to go back")
                    .AddChoices(carOptions)
            );

            if (selectedCarOption == "Cancel") return null;

            int carId = int.Parse(selectedCarOption.Split('.')[0]);
            return availableCars.SingleOrDefault(c => c.Id == carId);
        }

        public (DateTime StartDate, DateTime EndDate)? GetReservationDates()
        {
            try
            {
                DateTime startDate = DateTime.Now;
                AnsiConsole.MarkupLine($"[green]Start date set to: {startDate.ToShortDateString()}[/]");
                var endDate = AnsiConsole.Ask<DateTime>("Enter end date (yyyy-mm-dd):");
                return (startDate, endDate);
            }
            catch
            {
                ShowMessage("Invalid date format.", "red");
                return null;
            }
        }

        public void DisplayReservations(List<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                var car = reservation.Car;
                var customer = reservation.Customer;

                if (car == null || customer == null)
                {
                    ShowMessage($"Reservation ID: {reservation.Id} has incomplete data.", "red");
                    continue;
                }

                AnsiConsole.MarkupLine($"[bold]Reservation ID: {reservation.Id}[/]");
                AnsiConsole.MarkupLine($"Car: [blue]{car.Marka} {car.Model}[/]");
                AnsiConsole.MarkupLine($"Reserved by: [cyan]{customer.Imie} {customer.Nazwisko}[/]");
                AnsiConsole.MarkupLine($"From: [yellow]{reservation.Poczatek.ToShortDateString()}[/] To: [yellow]{reservation.Koniec.ToShortDateString()}[/]");
                AnsiConsole.MarkupLine("[grey]---------------------------------------------------[/]");
            }
        }

        public Reservation SelectReservationToDelete(List<Reservation> reservations)
        {
            var reservationOptions = reservations
                .Select(r => $"ID: {r.Id}, Car: {r.Car.Marka} {r.Car.Model}, Reserved by: {r.Customer.Imie} {r.Customer.Nazwisko}")
                .ToList();
            reservationOptions.Add("Cancel");

            var selectedReservationOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a reservation to delete or choose 'Cancel' to go back")
                    .AddChoices(reservationOptions)
            );

            if (selectedReservationOption == "Cancel") return null;

            int reservationId = int.Parse(selectedReservationOption.Split(',')[0].Split(':')[1].Trim());
            return reservations.SingleOrDefault(r => r.Id == reservationId);
        }

        public bool ConfirmDeletion(Reservation reservation)
        {
            return AnsiConsole.Confirm($"Are you sure you want to delete Reservation ID: {reservation.Id}?");
        }

        public void ShowMessage(string message, string color)
        {
            AnsiConsole.MarkupLine($"[{color}]{message}[/]");
        }
    }
}
