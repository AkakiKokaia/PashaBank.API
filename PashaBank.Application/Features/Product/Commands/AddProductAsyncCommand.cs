using MediatR;
using PashaBank.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.Product.Commands
{
    public sealed record AddProductAsyncCommand(string ProductCode, string ProductName, decimal PricePerUnit): IRequest<Response<bool>>;
}
