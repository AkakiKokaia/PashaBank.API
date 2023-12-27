using MediatR;
using PashaBank.Application.DTOs.User;
using PashaBank.Application.Wrappers;

namespace PashaBank.Application.Features.Account.Queries.Users
{
    public sealed record GetUserByIdAsyncQuery(Guid Id) : IRequest<Response<GetUserResponse>>;
}
