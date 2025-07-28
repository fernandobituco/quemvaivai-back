using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object id)
            : base($"{entityName} com ID '{id}' não foi encontrado.") { }
        public NotFoundException(string entityName)
            : base($"{entityName} não foi encontrado.") { }
    }
}