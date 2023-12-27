using Microsoft.AspNetCore.Mvc;
using PashaBank.Application.Features.Product.Commands;
using PashaBank.Application.Features.ProductSales.Commands;

namespace PashaBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSalesController : BaseApiController
    {
        [HttpPost(nameof(CreateProductSale))]
        public async Task<IActionResult> CreateProductSale(AddProductSaleAsyncCommand request) => Ok(await Mediator.Send(request));
    }
}
