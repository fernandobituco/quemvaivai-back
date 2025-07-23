using QuemVaiVai.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class GroupUser : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Role como int para representar enum: 0 = admin, 1 = member
        public Role Role { get; set; }
    }
}
