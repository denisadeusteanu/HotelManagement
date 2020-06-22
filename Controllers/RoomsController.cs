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

        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {
            try
            {
                return Ok(_repository.GetAllRooms());
            }
            catch (Exception)
            {
                return BadRequest("Nu s-au putut gasi camerele!");
            }
        }

        //[HttpGet]
        //public IActionResult Get(int id)
        //{
        //    try
        //    {
        //        var room = _repository.GetRoomById(id);

        //        if (room != null) return Ok(_mapper.Map<Room, RoomManagementViewModel>(room));
        //        else return NotFound();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Failed to get room {id}");
        //    }
        //}

        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _repository.DeleteRoomById(id);
                if (_repository.SaveAll())
                {
                    return RedirectToAction("RoomManagement", "App");
                }

            }
            catch (Exception)
            {
                return BadRequest($"Failed to delete room {id}");
            }
            return BadRequest($"Failed to delete room {id}");
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] RoomManagementViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            model.HotelId = _repository.GetHotel().Id;
        //            var newRoom = _mapper.Map<RoomManagementViewModel, Room>(model);

        //            _repository.AddEntity(newRoom);
        //            if (_repository.SaveAll())
        //            {
        //                return Created($"/api/rooms/{newRoom.Id}", _mapper.Map<Room, RoomManagementViewModel>(newRoom));
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Camera nu a putut fi salvata.");
        //    }

        //    return BadRequest("Camera nu a putut fi salvata.");
        //}

        [HttpPost("id")]
        public IActionResult Add([FromForm] Room model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.HotelId = _repository.GetHotel().Id;

                    _repository.AddEntity(model);
                    if (_repository.SaveAll())
                    {
                        // return Created($"/api/rooms/{model.Id}", model);
                        return RedirectToAction("RoomManagement", "App");
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

        [HttpPost]
        public IActionResult Edit([FromForm]Room model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.ModifyRoomState(model);
                    return RedirectToAction("RoomManagement", "App");
                   
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Modificarile nu au putut fi salvate.");
            }
        }

        [Route("id")]
        public IActionResult EditRoom(int id)
        {
            return View(_repository.GetRoomById(id));
        }

        public IActionResult CreateRoom()
        {
            return View();
        }
    }
}
