using System.ComponentModel.DataAnnotations;

namespace HotelManagement.ViewModels
{
    public class GuestViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}