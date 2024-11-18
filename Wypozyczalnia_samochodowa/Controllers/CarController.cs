using System;
using System.Linq;
using Spectre.Console;
using Wypozyczalnia_samochodowa.Data;
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
            _carView = new CarView(this);
        }

        public void ManageCars()
        {
            while (true)
            {
                var selectedOption = _carView.ShowManageCarsMenu();

                switch (selectedOption)
                {
                    case "List Cars":
                        ShowAvailableCars();
                        PauseBeforeReturning();
                        break;
                    case "Add Car":
                        AddCar();
                        PauseBeforeReturning();
                        break;
                    case "Update Car":
                        UpdateCar();
                        PauseBeforeReturning();
                        break;
                    case "Delete Car":
                        DeleteCar();
                        PauseBeforeReturning();
                        break;
                    case "Return to Main Menu":
                        return;
                }
            }
        }

        public void ShowAvailableCars()
        {
            var availableCars = _context.Cars.Where(c => c.Dostepnosc).ToList();

            if (!availableCars.Any())
            {
                _carView.ShowErrorMessage("No cars available.");
            }
            else
            {
                _carView.DisplayAvailableCars(availableCars);
            }
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
        public void PauseBeforeReturning()
        {
            AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
            Console.ReadKey(true);
        }
    }
}
