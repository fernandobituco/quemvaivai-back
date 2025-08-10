namespace QuemVaiVai.Domain.Responses;

public record LoginResponse(string AccessToken, DateTime AccessTokenExpiry);