
namespace QuemVaiVai.Domain.Entities
{
    public class VoteOption : BaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime? Date { get; set; }
        public string? Location { get; set; }
        public bool Active { get; set; }

        public ICollection<Vote> Votes { get; set; } = [];
    }
}
