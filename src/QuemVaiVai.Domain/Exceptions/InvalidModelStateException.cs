
namespace QuemVaiVai.Domain.Exceptions
{
    public class InvalidModelStateException(string message = "Dados inválidos.") : Exception(message);
}
