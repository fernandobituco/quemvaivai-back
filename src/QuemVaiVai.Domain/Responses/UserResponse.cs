using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Responses
{
    public record UserResponse(int Id, string Name, string Email);
}
