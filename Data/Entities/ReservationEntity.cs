using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.Entities
{
    public class ReservationEntity
    {
       
        public int Id { get; set; }
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
    }
}
