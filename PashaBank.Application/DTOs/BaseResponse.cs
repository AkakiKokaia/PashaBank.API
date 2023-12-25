using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Application.DTOs
{
    public class BaseResponse
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual int? CreatedById { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
        public virtual int? UpdatedById { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
