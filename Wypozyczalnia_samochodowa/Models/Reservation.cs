using System;

namespace Wypozyczalnia_samochodowa.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Poczatek { get; set; }
        public DateTime Koniec { get; set; }

        public override string ToString()
        {
            return $"Reservation {Id}: {Car} rented by {Customer} from {Poczatek.ToShortDateString()} to {Koniec.ToShortDateString()}";
        }
    }
}
