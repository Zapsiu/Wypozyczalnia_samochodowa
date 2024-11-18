using System;

namespace Wypozyczalnia_samochodowa.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Siedzenia { get; set; }
        public bool Dostepnosc { get; set; }
        public int Cena { get; set; }

        public override string ToString()
        {
            return $"{Marka} {Model}, Seats: {Siedzenia}, Available: {Dostepnosc}, Price: {Cena} zł";
        }
    }
}
