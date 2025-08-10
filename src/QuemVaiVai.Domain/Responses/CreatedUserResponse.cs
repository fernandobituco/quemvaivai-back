namespace QuemVaiVai.Domain.Responses;

public record CreatedUserResponse(int Guid, string Name, string Email, DateTime CreatedAt);