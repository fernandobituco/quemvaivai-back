using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Responses
{
    public record GroupResponse(int Id, string Name, string? Description);
}
