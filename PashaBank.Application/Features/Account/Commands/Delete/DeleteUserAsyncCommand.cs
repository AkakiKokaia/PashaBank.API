using MediatR;
using PashaBank.Application.Wrappers;

namespace PashaBank.Application.Features.Account.Commands.Delete
{
    public class DeleteUserAsyncCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }
}
