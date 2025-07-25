using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
