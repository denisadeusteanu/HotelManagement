using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Entities
{
    public class Hotel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public int NrOfRooms { get; set; }
       // public ICollection<User> Users { get; set; }
    }
}
