using HotelManagement.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ViewModels
{
    public class RoomManagementViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        [Required]
        public int PersonCapacity { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        public DaNu IsOccupied { get; set; }
        public DaNu IsUsable { get; set; }
        [MaxLength(250, ErrorMessage = "Descrierea este prea lunga.")]
        public string Description { get; set; }
        //[Required]
        //[MaxLength(250, ErrorMessage ="Too long.")]
        //public string RoomDescription { get; set; }
    }
}
