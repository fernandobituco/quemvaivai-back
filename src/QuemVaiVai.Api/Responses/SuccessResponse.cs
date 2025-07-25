namespace QuemVaiVai.Api.Responses;

public record SuccessResponse<T>(bool Success, T Data);