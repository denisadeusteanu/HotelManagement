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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationsController : Controller
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
            try
            {
                return Ok(_mapper.Map<IEnumerable<Reservation>,
                    IEnumerable<ReservationManagementViewModel>>
                    (_repository.GetAllReservations()));
            }
            catch (Exception)
            {
                return BadRequest("Nu s-au putut gasi rezervarile!");
            }

        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetReservationById(int id)
        {
            try
            {
                var reservation = _repository.GetReservationById(id);

                if (reservation != null)
                {
                    return Ok(_mapper.Map<Reservation, ReservationManagementViewModel>(reservation));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get reservation {id}");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ReservationManagementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateReservation(_mapper.Map<ReservationManagementViewModel, Reservation>(model));
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update reservation {model.Id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ReservationManagementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.NrOfNights = (model.CheckOutDate - model.CheckinDate).Days;

                    var newReservation = _mapper.Map<ReservationManagementViewModel, Reservation>(model);

                    if (newReservation.CheckinDate < DateTime.Now.AddDays(-1) || newReservation.CheckOutDate < DateTime.Now)
                    {
                        return BadRequest("Datele nu pot fi din trecut!");
                    }

                    _repository.AddEntity(newReservation);

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
            catch (Exception ex)
            {
                return BadRequest("Rezervarea nu a putut fi salvata.");
            }
            return BadRequest("Rezervarea nu a putut fi salvata.");
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteReservationById(id);

                if (_repository.SaveAll())
                {
                    return Ok();
                }

                return BadRequest($"Failed to delete reservation {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete reservation {id}");
            }
        }
    }
}
