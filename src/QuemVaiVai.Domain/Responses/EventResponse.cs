
namespace QuemVaiVai.Domain.Responses
{
    public record EventResponse(int Id, string Title, string? Description, string? location, DateTime? EventDate, string? GroupName);
}
