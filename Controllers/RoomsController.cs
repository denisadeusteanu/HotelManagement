using AutoMapper;
using HotelManagement.Data;
using HotelManagement.Data.Entities;
using HotelManagement.ViewModels;
using Microsoft.AspNetCore.Cors;
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
    public class RoomsController : Controller
    {
        private readonly IHotelRepository _repository;
        private readonly IMapper _mapper;

        public RoomsController(IHotelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Room>> Get()
        {
            try { 
            return Ok(_repository.GetAllRooms());
            }
            catch (Exception)
            {
                return BadRequest("Nu s-au putut gasi camerele!");
            }
        }

        public IActionResult Get(int id)
        {
            try
            {
                var room = _repository.GetRoomById( id);

                if (room != null) return Ok(_mapper.Map<Room, RoomManagementViewModel>(room));
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get room {id}");
            }
        }

        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteRoomById(id);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("Index", "App");
                }

            }
            catch (Exception)
            {
                return BadRequest($"Failed to delete room {id}");
            }
            return BadRequest($"Failed to delete room {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] RoomManagementViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newRoom = _mapper.Map<RoomManagementViewModel, Room>(model);

                    _repository.AddEntity(newRoom);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/rooms/{newRoom.Id}", _mapper.Map<Room, RoomManagementViewModel>(newRoom));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Camera nu a putut fi salvata.");
            }

            return BadRequest("Camera nu a putut fi salvata.");
        }

        [Route("id")]
        public IActionResult RoomPopUp(int id)
        {
            return View();
        }
    }
}
