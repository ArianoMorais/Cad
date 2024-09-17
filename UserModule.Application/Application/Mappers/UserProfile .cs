using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Domain.Dtos;
using UserModule.Domain.Entities;

namespace UserModule.Application.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<AdressDto, Address>();

            CreateMap<User, UserDto>();
            CreateMap<Address, AdressDto>();
        }
    }
}
