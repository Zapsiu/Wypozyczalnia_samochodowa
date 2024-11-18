namespace Wypozyczalnia_samochodowa.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } 

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({Username})";
        }
    }
}
