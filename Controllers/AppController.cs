using HotelManagement.Data;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    public class AppController : Controller
    {
        private readonly IHotelRepository _repository;

        public AppController(IHotelRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("MainPage")]
        public IActionResult MainPage()
        {
            return View();
        }

        [HttpGet("RoomManagement")]
        public IActionResult RoomManagement()
        {
            var results = _repository.GetAllRooms();
            return View(results);
        }
        [HttpPost("RoomManagement")]
        public IActionResult RoomManagement(RoomManagementViewModel model)
        {
            if(ModelState.IsValid)
            {
                //do smth
            }
           else
            {
                //smth else
            }
            return View();
        }

        [HttpGet("ReservationManagement")]
        public IActionResult ReservationManagement()
        {
            return View();
        }

        [HttpGet("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }
    }
}
