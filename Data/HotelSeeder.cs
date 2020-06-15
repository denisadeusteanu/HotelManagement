using HotelManagement.Data.Entities;
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

            if (!_context.Rooms.Any()& !_context.Hotels.Any())
            {
                
                //sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/rooms.json");
                var json = File.ReadAllText(filepath);
                var rooms = JsonConvert.DeserializeObject<IEnumerable<Room>>(json);

                var hotel = _context.Hotels.Add(
                    new Hotel()
                    {
                        Name = "Blue Lagoon",
                        Rooms = (ICollection<Room>)rooms,
                        NrOfRooms = 6
                    });

                _context.Rooms.AddRange(rooms);

                
                _context.SaveChanges();
            }
        }
    }
}
