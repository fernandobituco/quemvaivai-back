namespace QuemVaiVai.Api.Responses;

public record CreatedUserResponse(int Guid, string Name, string Email, DateTime CreatedAt);