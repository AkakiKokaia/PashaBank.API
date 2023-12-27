using AutoMapper;
using PashaBank.Application.DTOs.Account;
using PashaBank.Application.DTOs.User;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PashaBank.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<RegisterAsyncCommand, UserEntity>().ReverseMap();
            CreateMap<UserEntity, GetUserResponse>().ReverseMap();
            CreateMap<LoginResponse, UserResponse>().ReverseMap();
            CreateMap<LoginResponse, UserEntity>().ReverseMap();
            CreateMap<UserEntity, UserResponse>();
        }
    }
}
