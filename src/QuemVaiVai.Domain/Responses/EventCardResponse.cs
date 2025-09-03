
using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Domain.Responses
{
    public record EventCardResponse(
        int Id,
        string Title,
        string? Description,
        string? Location,
        DateTime? EventDate,
        Guid InviteCode,
        string? GroupName,
        int? GroupId,
        int Interested,
        int Going,
        bool CanEdit = true,
        bool ActiveVote = false,
        bool UserHasTaskItem = false,
        Status? Status = null
        );
}
