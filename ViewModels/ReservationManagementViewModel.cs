using HotelManagement.Data.Entities;
using HotelManagement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class ReservationManagementViewModel
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        [Required]
        public DateTime CheckinDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public ReservationState ReservationState { get; set; }
        [Required]
        public GuestViewModel Guest { get; set; }
        public int NrOfNights { get; set; }
    }
}
