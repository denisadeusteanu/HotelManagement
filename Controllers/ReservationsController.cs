using AutoMapper;
using HotelManagement.Data;
using HotelManagement.Data.Entities;
using HotelManagement.Enums;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationsController :Controller
    {
        private readonly IHotelRepository _repository;
        private readonly IMapper _mapper;

        public ReservationsController(IHotelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try { 
            return Ok(_mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationManagementViewModel>>(_repository.GetAllReservations()));
            }
            catch(Exception)
            {
                return BadRequest("Nu s-au putut gasi rezervarile!");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]ReservationManagementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newReservation = _mapper.Map<ReservationManagementViewModel, Reservation>(model);

                    if (newReservation.CheckinDate < DateTime.Now.AddDays(-1) || newReservation.CheckOutDate < DateTime.Now)
                    {
                        return BadRequest("Datele nu pot fi din trecut!");
                    }

                    _repository.AddEntity(model);

                    if (_repository.SaveAll())
                    {
                        return Created($"api/reservations/{newReservation.Id}", _mapper.Map<Reservation, ReservationManagementViewModel>(newReservation));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception)
            {
                return BadRequest("Rezervarea nu a putut fi salvata.");
            }
            return BadRequest("Rezervarea nu a putut fi salvata.");
        }
    }
}
