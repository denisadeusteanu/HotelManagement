using HotelManagement.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Entities
{
    public class Room
    {
       
        public int Id { get; set; }
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Acest camp nu poate fi gol.")]
        public int PersonCapacity { get; set; }
        [Required(ErrorMessage = "Acest camp nu poate fi gol.")]
        public int RoomNumber { get; set; }
        [Required(ErrorMessage = "Trebuie selectata o optiune.")]
        public DaNu IsOccupied { get; set; }
        [Required(ErrorMessage = "Trebuie selectata o optiune.")]
        public DaNu IsUsable { get; set; }
        [MaxLength(250, ErrorMessage = "Descrierea este prea lunga.")]
        public string Description { get; set; }
     // public ICollection<Reservation> Reservations { get; set; }
    }
}
