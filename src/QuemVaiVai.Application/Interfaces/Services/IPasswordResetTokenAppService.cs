namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IPasswordResetTokenAppService
    {
        Task Generate(string email);
        Task Validate(string token, int userId);
    }
}
