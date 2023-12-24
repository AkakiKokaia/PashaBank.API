using PashaBank.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBank.Domain.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool Revoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }
    }
}
