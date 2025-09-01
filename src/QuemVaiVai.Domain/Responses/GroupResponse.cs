
namespace QuemVaiVai.Domain.Responses
{
    public record GroupResponse(int Id, string Name, string? Description, int MemberCount, Guid? InviteCode = null);
}
