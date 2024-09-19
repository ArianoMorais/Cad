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
            CreateMap<LoginDto, User>();
            CreateMap<User, LoginDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<AdressDto, Address>();

            CreateMap<Address, AdressDto>();
        }
    }
}
