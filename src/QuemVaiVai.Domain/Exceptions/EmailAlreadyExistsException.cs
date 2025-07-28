using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Exceptions
{
    public class EmailAlreadyExists(string message = "Esse email não está disponível.") : Exception(message);
}
