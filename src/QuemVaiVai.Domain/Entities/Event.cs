
namespace QuemVaiVai.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? EventDate { get; set; }
        public Guid InviteCode { get; } = Guid.NewGuid();

        public int? GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<UserEvent> UserEvents { get; set; } = [];
        public ICollection<VoteOption> VoteOptions { get; set; } = [];
    }
}
