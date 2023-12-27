using PashaBank.Domain.Entities.Common;

namespace PashaBank.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal PricePerUnit { get; set; }

    }
}
