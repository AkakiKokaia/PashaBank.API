using AutoMapper;
using PashaBank.Application.DTOs.Account;
using PashaBank.Application.DTOs.User;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Features.Product.Commands;
using PashaBank.Application.Features.ProductSales.Commands;
using PashaBank.Domain.Entities;

namespace PashaBank.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<RegisterAsyncCommand, UserEntity>().ReverseMap();
            CreateMap<AddProductAsyncCommand, ProductEntity>().ReverseMap();
            CreateMap<AddProductSaleAsyncCommand, ProductSalesEntity>().ReverseMap();


            CreateMap<UserEntity, GetUserResponse>().ReverseMap();
            CreateMap<LoginResponse, UserResponse>().ReverseMap();
            CreateMap<LoginResponse, UserEntity>().ReverseMap();
            CreateMap<UserEntity, UserResponse>();
        }
    }
}
