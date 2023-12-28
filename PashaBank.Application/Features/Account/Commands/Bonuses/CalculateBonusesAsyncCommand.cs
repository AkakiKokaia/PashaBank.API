using MediatR;
using PashaBank.Application.Wrappers;
using PashaBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Account.Commands.Bonuses
{
    public sealed record CalculateBonusesAsyncCommand(DateTimeOffset? startDate, DateTimeOffset? endDate) : IRequest<Response<List<CalculateBonusesAsyncResponseItem>>>;
}
