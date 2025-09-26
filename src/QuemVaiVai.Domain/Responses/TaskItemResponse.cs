
namespace QuemVaiVai.Domain.Responses
{
    public record TaskItemResponse(int Id, string Description, int? AssignedTo, bool IsDone);
}
