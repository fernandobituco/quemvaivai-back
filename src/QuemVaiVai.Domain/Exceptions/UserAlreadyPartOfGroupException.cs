using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Exceptions
{
    public class UserAlreadyPartOfGroupException(string message = "Esse usuário já é parte desse grupo.") : Exception(message);
}
