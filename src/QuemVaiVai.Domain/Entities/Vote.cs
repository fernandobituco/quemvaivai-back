
namespace QuemVaiVai.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public int VoteOptionId { get; set; }
        public VoteOption VoteOption { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
