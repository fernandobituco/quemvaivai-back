using QuemVaiVai.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class UserEvent : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public Status Status { get; set; }
        public Role Role { get; set; }
    }
}
