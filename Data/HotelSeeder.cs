using HotelManagement.Data.Entities;
using HotelManagement.Enums;
using Microsoft.AspNetCore.Hosting;
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

        public HotelSeeder(HotelContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Rooms.Any() & !_context.Hotels.Any())
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

                _context.Reservations.Add(
                   new Reservation()
                   {
                       CheckinDate = DateTime.UtcNow.AddDays(-4),
                       CheckOutDate = DateTime.UtcNow.AddDays(-1),
                       ReservationState = ReservationState.Reserved,
                       Guest = guests.FirstOrDefault(),
                       NrOfNights = 3
                   });

                _context.SaveChanges();

                _context.Reservations.FirstOrDefault().ReservationEntities = new List<ReservationEntity>
                {
                         new ReservationEntity()
                         {
                             Room=rooms.FirstOrDefault(),
                         }
                };

                _context.Rooms.AddRange(rooms);

                _context.SaveChanges();
            }
        }
    }
}
