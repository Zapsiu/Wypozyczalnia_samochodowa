using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia_samochodowa.Data;
using Wypozyczalnia_samochodowa.Models;
using Wypozyczalnia_samochodowa.Views;

namespace Wypozyczalnia_samochodowa.Controllers
{
    public class ReservationController
    {
        private readonly RentalDbContext _context;
        private readonly ReservationView _reservationView;

        public ReservationController(RentalDbContext context)
        {
            _context = context;
            _reservationView = new ReservationView(this);
        }

        public void ReserveCarForUser(object selectedCar, DateTime value, Customer currentUser)
        {
            var availableCars = _context.Cars.Where(c => c.Dostepnosc).ToList();
            if (!availableCars.Any())
            {
                _reservationView.ShowMessage("No available cars for reservation.", "yellow");
                return;
            }

            var filteredCars = _reservationView.FilterAndSortCars(availableCars);
            if (!filteredCars.Any())
            {
                _reservationView.ShowMessage("No cars match the selected filters.", "yellow");
                return;
            }

            if (_reservationView.SelectCarToReserve(filteredCars) == null)
            {
                _reservationView.ShowMessage("Reservation canceled.", "grey");
                return;
            }

            var reservationDates = _reservationView.GetReservationDates();
            if (reservationDates == null) return;

            var reservation = new Reservation
            {
                Car = _reservationView.SelectCarToReserve(filteredCars),
                Customer = currentUser,
                Poczatek = reservationDates.Value.StartDate,
                Koniec = reservationDates.Value.EndDate
            };

            _reservationView.SelectCarToReserve(filteredCars).Dostepnosc = false;
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            _reservationView.ShowMessage("Reservation created successfully.", "green");
        }

        public void DisplayReservations()
        {
            var reservations = _context.Reservations
                                       .Include(r => r.Car)
                                       .Include(r => r.Customer)
                                       .ToList();

            if (!reservations.Any())
            {
                _reservationView.ShowMessage("No reservations found.", "yellow");
                return;
            }

            _reservationView.DisplayReservations(reservations);
        }

        public void DeleteReservation()
        {
            var reservations = _context.Reservations
                                       .Include(r => r.Car)
                                       .Include(r => r.Customer)
                                       .ToList();

            if (!reservations.Any())
            {
                _reservationView.ShowMessage("No reservations available for deletion.", "yellow");
                return;
            }

            var reservationToDelete = _reservationView.SelectReservationToDelete(reservations);
            if (reservationToDelete == null)
            {
                _reservationView.ShowMessage("Delete action canceled.", "grey");
                return;
            }

            if (_reservationView.ConfirmDeletion(reservationToDelete))
            {
                reservationToDelete.Car.Dostepnosc = true;
                _context.Reservations.Remove(reservationToDelete);
                _context.SaveChanges();
                _reservationView.ShowMessage("Reservation deleted successfully.", "green");
            }
            else
            {
                _reservationView.ShowMessage("Delete action canceled.", "yellow");
            }
        }
        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations
                           .Include(r => r.Car)
                           .Include(r => r.Customer)
                           .ToList();
        }

        public void ReserveCarForUser(object selectedCar, DateTime value, CustomerController customerController)
        {
            throw new NotImplementedException();
        }
    }
}
