using PashaBank.Domain.Entities.Common;

namespace PashaBank.Domain.Entities
{
    public class ProductSalesEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTimeOffset DateOfSale { get; set; }
        public Guid ProductId { get; set; }
        public decimal SellPrice { get; set; }  
        public decimal TotalPrice { get; set; }
        public bool WasCalculated { get; set; }

        public virtual ProductEntity Product { get; set; }
        public virtual UserEntity User { get; set; }

        public void SetWasCalculated()
        {
            WasCalculated = true;
        }
    }
}
