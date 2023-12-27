using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.ProductSales.Queries
{
    public sealed record GetProductSalesAsyncQuery(long? PersonalNumber, DateTimeOffset? DateOfSale, Guid? ProductId, int Page = 1, int PageSize = 25) : IRequest<GetProductSalesAsyncQueryResponse>;
}
