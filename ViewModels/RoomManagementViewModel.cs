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
       // [Required]
        public int PersonCapacity { get; set; }
       // [Required]
        public int RoomNumber { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsUsable { get; set; }
        [MaxLength(250, ErrorMessage = "Too long.")]
        public string Description { get; set; }
        //[Required]
        //[MaxLength(250, ErrorMessage ="Too long.")]
        //public string RoomDescription { get; set; }
    }
}
