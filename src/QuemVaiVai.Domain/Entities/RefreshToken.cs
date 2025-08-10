using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRevoked { get; set; }
        public string? ReplacedByToken { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;
        public bool IsActive => !IsRevoked && !IsExpired;

        public void Revoke(string? reason = null)
        {
            if (IsRevoked) return;
            IsRevoked = true;
        }

        public static RefreshToken Create(string token, int userId, TimeSpan validity)
        {
            return new RefreshToken
            {
                Token = token,
                UserId = userId,
                ExpiryDate = DateTime.UtcNow.Add(validity),
                CreatedDate = DateTime.UtcNow,
            };
        }
    }
}
