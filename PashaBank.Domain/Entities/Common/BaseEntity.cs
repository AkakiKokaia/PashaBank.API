using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PashaBank.Domain.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual Guid? CreatedById { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual Guid? UpdatedById { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool IsDeleted { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            DeletedAt = null;
            IsDeleted = false;
        }
    }
}
