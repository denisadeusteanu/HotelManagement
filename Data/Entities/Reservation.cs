using HotelManagement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Data.Entities
{
    public class Reservation
    {
        
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationState ReservationState { get; set; }
        public Guest Guest { get; set; }
        public int NrOfNights { get; set; }
    }
}
