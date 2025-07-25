using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
