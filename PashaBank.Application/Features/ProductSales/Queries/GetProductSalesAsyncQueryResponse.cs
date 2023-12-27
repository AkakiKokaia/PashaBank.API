using PashaBank.Application.Features.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.Features.ProductSales.Queries
{
    public class GetProductSalesAsyncQueryResponse : PagedResponseBase
    {
        public GetProductSalesAsyncQueryResponse(List<GetProductSalesAsyncQueryResponseItem> productSales, int page, int pageSize, int totalCount)
            : base(page, pageSize, totalCount)
        {
            ProductSales = productSales;
        }

        public List<GetProductSalesAsyncQueryResponseItem> ProductSales { get; }
    }

    public class GetProductSalesAsyncQueryResponseItem
    {
        public DateTimeOffset DateOfSale { get; set; }
        public decimal SellPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public GetProductAsyncQueryResponseItem Product { get; set; }
        public GetUserAsyncQueryResponseItem User { get; set; }
    }
    
    public class GetProductAsyncQueryResponseItem
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal PricePerUnit { get; set; }
    }

    public class GetUserAsyncQueryResponseItem
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public long PersonalNumber { get; set; }
    }
}
