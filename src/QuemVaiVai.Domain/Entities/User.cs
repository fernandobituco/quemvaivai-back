using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool Confirmed { get; set; } = false;

        public ICollection<GroupUser> GroupUsers { get; set; } = [];
        public ICollection<UserEvent> UserEvents { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<TaskItem> TaskItems { get; set; } = [];
        public ICollection<Vote> Votes { get; set; } = [];
    }
}
