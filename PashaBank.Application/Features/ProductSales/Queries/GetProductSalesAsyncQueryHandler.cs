using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PashaBank.Domain.Interfaces;
using PashaBank.Infrastructure;

namespace PashaBank.Application.Features.ProductSales.Queries
{
    public class GetProductSalesAsyncQueryHandler : IRequestHandler<GetProductSalesAsyncQuery, GetProductSalesAsyncQueryResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PashaBankDbContext _dbContext;

        public GetProductSalesAsyncQueryHandler(IUnitOfWork uow, IMapper mapper, IHttpContextAccessor httpContextAccessor, PashaBankDbContext dbContext)
        {
            _uow = uow;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<GetProductSalesAsyncQueryResponse> Handle(GetProductSalesAsyncQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.ProductSales
                    .Where(x =>
                        (!request.PersonalNumber.HasValue || x.User.PersonalNumber == request.PersonalNumber.Value) &&
                        (!request.DateOfSale.HasValue || x.DateOfSale == request.DateOfSale.Value) &&
                        (!request.ProductId.HasValue || x.ProductId == request.ProductId.Value)
                    )
                    .OrderBy(x => x.Id);

            var totalCount = await query.CountAsync();

            var productSale = await query
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetProductSalesAsyncQueryResponseItem
                {
                    DateOfSale = x.DateOfSale,
                    SellPrice = x.SellPrice,
                    TotalPrice = x.TotalPrice,
                    Product = new GetProductAsyncQueryResponseItem
                    {
                        Id = x.Product.Id,
                        ProductCode = x.Product.ProductCode,
                        ProductName = x.Product.ProductName,
                        PricePerUnit = x.Product.PricePerUnit
                    },
                    User = new GetUserAsyncQueryResponseItem
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        Surname = x.User.Surname,
                        PersonalNumber = x.User.PersonalNumber
                    }
                })
                .ToListAsync();


            return new GetProductSalesAsyncQueryResponse(productSale, request.Page, request.PageSize, totalCount);
        }
    }
}
