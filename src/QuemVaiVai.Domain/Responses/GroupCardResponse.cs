using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Responses
{
    public record GroupCardResponse(int Id, string Name, string? Description, int MemberCount, int EventCount, DateTime? NextEventDate, bool CanEdit, Guid InviteCode);
}
