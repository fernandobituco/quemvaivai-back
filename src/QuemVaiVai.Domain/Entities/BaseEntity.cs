using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedUser { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
        public int UpdatedUser { get; set; }
        public DateTime? DeletedAt { get; set; } = null;
        public int DeletedUser { get; set; }
        public bool Deleted { get; set; }
        public void UpdateTimestamp(int? userId = null)
        {
            UpdatedAt = DateTime.UtcNow;
            if (userId.HasValue)
            {
                UpdatedUser = userId.Value;
            }
        }
    }
}
