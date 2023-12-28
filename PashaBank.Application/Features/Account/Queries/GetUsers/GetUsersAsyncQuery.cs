using MediatR;
using PashaBank.Application.Features.ProductSales.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Account.Queries.GetUsers
{
    public sealed record GetUsersAsyncQuery(int Page = 1, int PageSize = 25) : IRequest<GetUsersAsyncQueryResponse>;
}
