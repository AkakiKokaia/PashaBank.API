using Microsoft.AspNetCore.Mvc;
using PashaBank.Application.Features.Account.Commands.Bonuses;
using PashaBank.Application.Features.Account.Commands.Delete;
using PashaBank.Application.Features.Account.Commands.Register;
using PashaBank.Application.Features.Account.Commands.Update;
using PashaBank.Application.Features.Account.Queries.GetUsers;
using PashaBank.Application.Wrappers;

namespace PashaBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterAsyncCommand request) => Ok(await Mediator.Send(request));

        [HttpGet(nameof(GetUsers))]
        public async Task<GetUsersAsyncQueryResponse> GetUsers([FromQuery] GetUsersAsyncQuery request) =>
                 await Mediator.Send(request);

        [HttpDelete(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserAsyncCommand request) => Ok(await Mediator.Send(request));

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserAsyncCommand request) => Ok(await Mediator.Send(request));

        [HttpPost(nameof(CalculateBonuses))]
        public async Task<IActionResult> CalculateBonuses([FromQuery] CalculateBonusesAsyncCommand request) => Ok(await Mediator.Send(request));
    }
}
