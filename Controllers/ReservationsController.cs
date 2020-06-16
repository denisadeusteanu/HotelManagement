using HotelManagement.Data;
using HotelManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ReservationsController :Controller
    {
        private readonly IHotelRepository _repository;

        public ReservationsController(IHotelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try { 
            return Ok(_repository.GetAllReservations());
            }
            catch(Exception)
            {
                return BadRequest("Nu s-au putut gasi rezervarile!");
            }

        }
    }
}
