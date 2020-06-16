using HotelManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data
{
    public class HotelContext :DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options):base(options)
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
