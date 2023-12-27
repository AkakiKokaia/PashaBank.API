using Microsoft.AspNetCore.Mvc;
using PashaBank.Application.Features.Product.Commands;

namespace PashaBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        [HttpPost(nameof(CreateProduct))]
        public async Task<IActionResult> CreateProduct(AddProductAsyncCommand request) => Ok(await Mediator.Send(request));
    }
}
