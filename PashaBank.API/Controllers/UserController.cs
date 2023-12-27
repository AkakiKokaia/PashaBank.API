using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Wrappers;

namespace PashaBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterAsyncCommand request) => Ok(await Mediator.Send(request));
    }
}
