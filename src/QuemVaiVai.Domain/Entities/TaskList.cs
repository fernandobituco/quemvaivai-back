using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class TaskList : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        public string Title { get; set; } = null!;

        public ICollection<TaskItem> TaskItems { get; set; }
    }
}
