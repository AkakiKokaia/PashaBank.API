using MediatR;
using PashaBank.Application.Wrappers;

namespace PashaBank.Application.Features.ProductSales.Commands
{
    public sealed record AddProductSaleAsyncCommand(Guid userId, DateTimeOffset dateOfSell, Guid productId, decimal sellPrice, decimal totalPrice) : IRequest<Response<bool>>;
}
