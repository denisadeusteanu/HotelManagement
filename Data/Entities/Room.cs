using HotelManagement.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Entities
{
    public class Room
    {
       
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int PersonCapacity { get; set; }
        public int RoomNumber { get; set; }
        public DaNu IsOccupied { get; set; }
        public DaNu IsUsable { get; set; }
        public string Description { get; set; }
     // public ICollection<Reservation> Reservations { get; set; }
    }
}
