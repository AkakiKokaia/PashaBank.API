﻿using AutoMapper;
using MediatR;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;

namespace PashaBank.Application.Features.ProductSales.Commands
{
    public class AddProductSaleAsyncCommandHandler : IRequestHandler<AddProductSaleAsyncCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AddProductSaleAsyncCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(AddProductSaleAsyncCommand request, CancellationToken cancellationToken)
        {
            var newProductSale = _mapper.Map<ProductSalesEntity>(request);
            await _uow.productSalesRepository.Add(newProductSale);
            return new Response<bool>(true, "Product sale succesfully added");
        }
    }
}
