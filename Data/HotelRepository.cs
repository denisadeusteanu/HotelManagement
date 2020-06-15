using HotelManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Data
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelContext _context;

        public HotelRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms
                .OrderBy(r => r.RoomNumber)
                .ToList();
        }

        public IEnumerable<Room> GetAllRoomsByPersonCapacity()
        {
            return _context.Rooms
                .OrderBy(r => r.PersonCapacity)
                .ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
