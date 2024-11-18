using Microsoft.EntityFrameworkCore;
using Wypozyczalnia_samochodowa.Models;

namespace Wypozyczalnia_samochodowa.Data
{
    public class RentalDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ZAPS\\SQLEXPRESS;Database=RentalDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
