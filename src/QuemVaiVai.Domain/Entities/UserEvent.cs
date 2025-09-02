using QuemVaiVai.Domain.Enums;

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
