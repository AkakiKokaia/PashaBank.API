using AutoMapper;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Features.Account.Commands.Update;
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
            CreateMap<UpdateUserAsyncCommand, UserEntity>().ReverseMap();
            CreateMap<AddProductAsyncCommand, ProductEntity>().ReverseMap();
            CreateMap<AddProductSaleAsyncCommand, ProductSalesEntity>().ReverseMap();
        }
    }
}
