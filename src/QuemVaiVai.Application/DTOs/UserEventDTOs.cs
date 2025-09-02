
using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Application.DTOs
{
    public class UserEventDTOs
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
    }
}
