using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Entities
{
    public class EmailConfirmationToken : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Used { get; set; } = false;
    }
}
