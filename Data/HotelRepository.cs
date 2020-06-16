using HotelManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.ReservationEntities)
                .ThenInclude(re=>re.Room)
                .OrderBy(r => r.CheckinDate)
                .ToList();
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
