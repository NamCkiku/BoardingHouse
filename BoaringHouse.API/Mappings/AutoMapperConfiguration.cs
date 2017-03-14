using AutoMapper;
using BoardingHouse.Entities.Models;
using BoaringHouse.API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoaringHouse.API.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Room, RoomViewModel>();
            });

        }
    }
}