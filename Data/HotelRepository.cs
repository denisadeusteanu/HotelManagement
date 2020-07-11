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

        public Hotel GetHotel()
        {
            return _context.Hotels.FirstOrDefault();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Room)
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

        public Room GetRoomById(int id)
        {
            return _context.Rooms
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }
        public void ModifyRoomState(Room room)
        {
            _context.Entry<Room>(room).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation model)
        {
            _context.Entry<Reservation>(model).State = EntityState.Modified;
            _context.Entry<Guest>(model.Guest).State = EntityState.Modified;
            _context.Entry<Room>(model.Room).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRoomById(int id)
        {
            var toDel = _context.Rooms
                .Where(r => r.Id == id)
                .FirstOrDefault();
            _context.Rooms.Remove(toDel);
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Where(r => r.Id == id)
                .FirstOrDefault();
        }

        public void DeleteReservationById(int id)
        {
            var reservation = _context.Reservations
                .Where(r => r.Id == id)
                .FirstOrDefault();

            _context.Reservations.Remove(reservation);
        }

        public void CreateReservation(Reservation model)
        {
            _context.Entry<Reservation>(model).State = EntityState.Added;
            _context.Entry<Guest>(model.Guest).State = EntityState.Added;
            _context.Entry<Room>(model.Room).State = EntityState.Unchanged;

            _context.SaveChanges();
        }
    }
}
