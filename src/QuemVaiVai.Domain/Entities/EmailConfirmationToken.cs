
namespace QuemVaiVai.Domain.Entities
{
    public class EmailConfirmationToken : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public bool Used { get; set; } = false;
    }
}
