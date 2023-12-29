using MediatR;
using PashaBank.Application.Wrappers;

namespace PashaBank.Application.Features.Account.Commands.Bonuses
{
    public sealed record CalculateBonusesAsyncCommand(DateTimeOffset? startDate, DateTimeOffset? endDate) : IRequest<Response<List<CalculateBonusesAsyncResponseItem>>>;
}
