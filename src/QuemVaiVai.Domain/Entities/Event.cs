using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }
    }
}
