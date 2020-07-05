using HotelManagement.Data.Entities;
using HotelManagement.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Data
{
    public class HotelSeeder
    {
        private readonly HotelContext _context;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<User> _userManager;

        public HotelSeeder(HotelContext context, IHostingEnvironment hosting, UserManager<User> userManager)
        {
            _context = context;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            User user = await _userManager.FindByEmailAsync("denisa@hotel.com");
            if (user == null)
            {
                user = new User()
                {
                    FirstName = "Denisa",
                    LastName = "Deusteanu",
                    Email = "denisa@hotel.com",
                    UserName = "denisa@hotel.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder.");
                }
            }

            if (!_context.Rooms.Any() && !_context.Hotels.Any())
            {

                //sample data
                var filepathRooms = Path.Combine(_hosting.ContentRootPath, "Data/rooms.json");
                var jsonRooms = File.ReadAllText(filepathRooms);
                var rooms = JsonConvert.DeserializeObject<IEnumerable<Room>>(jsonRooms);

                var filepathGuests = Path.Combine(_hosting.ContentRootPath, "Data/guests.json");
                var jsonGuests = File.ReadAllText(filepathGuests);
                var guests = JsonConvert.DeserializeObject<IEnumerable<Guest>>(jsonGuests);
                _context.Guests.AddRange(guests);

                _context.Hotels.Add(
                   new Hotel()
                   {
                       Name = "Blue Lagoon",
                       Rooms = (ICollection<Room>)rooms,
                       NrOfRooms = 6
                   });
                _context.Rooms.AddRange(rooms);
                _context.SaveChanges();

                if (!_context.Reservations.Any())
                {
                    var reservation = new Reservation()
                    {
                        Room=rooms.First(),
                        CheckinDate = DateTime.UtcNow.AddDays(-4),
                        CheckOutDate = DateTime.UtcNow.AddDays(-1),
                        ReservationState = ReservationState.Reserved,
                        Guest = guests.First(),
                        NrOfNights = 3
                    };

                    _context.Reservations.Add(reservation);

                    _context.SaveChanges();
                }
            }
        }
    }
}
