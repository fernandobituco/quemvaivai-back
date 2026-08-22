
namespace QuemVaiVai.Domain.Entities
{
    public class PasswordResetToken : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string TokenHash { get; set; } = string.Empty!;
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; } = false;
    }
}
