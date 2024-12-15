using System;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;
using Wypozyczalnia_samochodowa.Views;

namespace Wypozyczalnia_samochodowa.Controllers
{
    public class CarController
    {
        private readonly RentalDbContext _context;
        private readonly CarView _carView;

        public CarController(RentalDbContext context)
        {
            _context = context;
            _carView = new CarView();
        }

        public void ManageCars()
        {
            while (true)
            {
                var action = _carView.ShowManageCarsMenu();

                switch (action)
                {
                    case "List Cars":
                        HandleListCars();
                        PauseAndClear();
                        break;

                    case "Add Car":
                        AddCar();
                        PauseAndClear();
                        break;

                    case "Update Car":
                        UpdateCar();
                        PauseAndClear();
                        break;

                    case "Delete Car":
                        DeleteCar();
                        PauseAndClear();
                        break;

                    case "Return to Main Menu":
                        return;

                    default:
                        _carView.ShowErrorMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public void PauseAndClear() {
            AnsiConsole.MarkupLine("[grey]Press any key to return...[/]");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        public void HandleListCars()
        {
            var choice = _carView.AskListAllOrFiltered();
            if (choice == "All")
            {
                DisplayAvailableCars();
            }
            else
            {
                DisplayAvailableCarsWithFilters();
            }
        }

        public void DisplayAvailableCars()
        {
            var cars = _context.Cars.ToList();
            _carView.DisplayAvailableCars(cars);
        }

        public void DisplayAvailableCarsWithFilters()
        {
            var cars = _context.Cars.Where(c => c.Dostepnosc).ToList();
            _carView.DisplayAvailableCarsWithFilters(cars);
        }

        public void AddCar()
        {
            var newCar = _carView.GetNewCarDetails();
            if (newCar == null) return;

            _context.Cars.Add(newCar);
            _context.SaveChanges();
            _carView.ShowSuccessMessage("Car added successfully.");
        }

        public void UpdateCar()
        {
            int carId = _carView.GetCarId("update");
            var car = _context.Cars.SingleOrDefault(c => c.Id == carId);

            if (car == null)
            {
                _carView.ShowErrorMessage("Car not found.");
                return;
            }

            var updatedCar = _carView.GetUpdatedCarDetails(car);
            if (updatedCar == null) return;

            _context.SaveChanges();
            _carView.ShowSuccessMessage("Car updated successfully.");
        }

        public void DeleteCar()
        {
            int carId = _carView.GetCarId("delete");
            var car = _context.Cars.SingleOrDefault(c => c.Id == carId);

            if (car == null)
            {
                _carView.ShowErrorMessage("Car not found.");
                return;
            }

            if (_carView.ConfirmDeletion(car))
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                _carView.ShowSuccessMessage("Car deleted successfully.");
            }
            else
            {
                _carView.ShowWarningMessage("Delete action canceled.");
            }
        }
    }
}
