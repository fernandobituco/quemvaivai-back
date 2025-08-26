
namespace QuemVaiVai.Domain.Exceptions
{
    public class RoleChangeNotAllowedException(string message = "Essa alteração de cargo não é permitida.") : Exception(message);
}
