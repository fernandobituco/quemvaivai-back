using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Exceptions
{
    public class UsedTokenException(string message = "Esse token já foi utilizado") : Exception(message);
}
