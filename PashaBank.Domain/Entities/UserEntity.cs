using Microsoft.AspNetCore.Identity;
using PashaBank.Domain.Enums;

namespace PashaBank.Domain.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public GenderTypeEnum Gender { get; set; }
        public string PhotoURL { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DocumentTypeEnum DocumentType { get; set; }
        public string? DocumentSeries { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTimeOffset DateOfIssue { get; set; }
        public DateTimeOffset DateOfExpiry { get; set; }
        public long PersonalNumber { get; set; }
        public string? IssuingAgency { get; set; }
        public ContactTypeEnum ContactType { get; set; }
        public string ContactInformation { get; set; }
        public AddressType AddressType { get; set; }
        public string Address { get; set; }
        public Guid? RecommendedById { get; set; }

        public virtual ICollection<UserRoleEntity> Roles { get; } = new List<UserRoleEntity>();
        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();
        public virtual ICollection<ProductSalesEntity> ProductSales { get; set; } = new List<ProductSalesEntity>();
    }
}
