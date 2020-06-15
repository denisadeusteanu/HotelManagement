using HotelManagement.Data.Entities;
using System.Collections.Generic;

namespace HotelManagement.Data
{
    public interface IHotelRepository
    {
        IEnumerable<Room> GetAllRooms();
        IEnumerable<Room> GetAllRoomsByPersonCapacity();
        bool SaveAll();
    }
}