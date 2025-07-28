
namespace QuemVaiVai.Domain.Exceptions
{
    public class UnauthorizedException(string message = "Acesso não autorizado.") : Exception(message);
}
