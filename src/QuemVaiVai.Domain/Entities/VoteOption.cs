using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class VoteOption : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime? SuggestedDate { get; set; }
        public string? SuggestedLocation { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
