using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedUser { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedUser { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
        public Guid DeletedUser { get; set; }
        public bool Deleted { get; set; }
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
