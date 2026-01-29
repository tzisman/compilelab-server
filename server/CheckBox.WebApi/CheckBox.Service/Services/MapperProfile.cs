using AutoMapper;
using CheckBox.Repository.Entities;
using CheckBox.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Services
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
    }
}
