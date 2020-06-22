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
                //.ForMember(dest => dest.RoomId, source => source.MapFrom(src => src.ReservationEntities.Select(re => re.Room.Id).First()))
                .ReverseMap();

            CreateMap<Room, RoomManagementViewModel>()
                .ReverseMap();
        }
    }
}
