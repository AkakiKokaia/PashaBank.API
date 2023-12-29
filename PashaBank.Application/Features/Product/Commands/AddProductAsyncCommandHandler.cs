using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PashaBank.Application.Exceptions;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Product.Commands
{
    public class AddProductAsyncCommandHandler : IRequestHandler<AddProductAsyncCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AddProductAsyncCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(AddProductAsyncCommand request, CancellationToken cancellationToken)
        {
            request.ProductCode.ToLower().Trim();
            var productExists = await _uow.productRepository.FindFirst(x => x.ProductCode == request.ProductCode);
            if (productExists != null)
            {
                throw new ApiException("Product with the same product code already exists");
            }
            var newProduct = _mapper.Map<ProductEntity>(request);
            await _uow.productRepository.Add(newProduct);
            return new Response<bool>(true, "Product succesfully added");
        }
    }
}
