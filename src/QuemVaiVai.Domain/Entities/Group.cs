
namespace QuemVaiVai.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid InviteCode { get; } = Guid.NewGuid();
        public ICollection<GroupUser> GroupUsers { get; set; } = [];
        public ICollection<Event> Events { get; set; } = [];
    }
}
