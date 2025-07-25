using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Tests.Responses.UserResponses
{
    public record CreatedUserResponse(int Id, string Name, string Email);
}
