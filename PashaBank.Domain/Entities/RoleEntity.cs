using Microsoft.AspNetCore.Identity;

namespace PashaBank.Domain.Entities
{
    public class RoleEntity : IdentityRole<Guid>
    {
        public override string Name { get; set; }
        public override string NormalizedName { get; set; }
        public virtual ICollection<UserRoleEntity> Users { get; } = new List<UserRoleEntity>();
    }
}
