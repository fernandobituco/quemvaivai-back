using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Domain.Responses
{
    public record UserMemberResponse(int Id, string Name, Role Role, Status? Status = null);
}
