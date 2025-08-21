using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Domain.Entities
{
    public class GroupUser : BaseEntity
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Role como int para representar enum: 0 = admin, 1 = member
        public Role Role { get; set; }
    }
}
