using AutoMapper;
using HotelManagement.Data.Entities;
using HotelManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Data
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Reservation, ReservationManagementViewModel>()              
                .ReverseMap();

            CreateMap<Guest, GuestViewModel>()
                .ReverseMap();

            CreateMap<Room, RoomManagementViewModel>()
                .ReverseMap();
        }
    }
}
