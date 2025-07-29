using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            : base($"Senha inválida.") { }
        public InvalidPasswordException(string message)
            : base($"Senha inválida: {message}") { }
    }
}
