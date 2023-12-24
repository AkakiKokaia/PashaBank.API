using PashaBank.Domain.Entities.Common;

namespace PashaBank.Domain.Entities
{
    public class ProductSalesEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTimeOffset DateOffSale { get; set; }
        public Guid ProductId {  get; set; }
        public decimal CostOfProduct { get; set; }
        public decimal CostOfPerUnit { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
