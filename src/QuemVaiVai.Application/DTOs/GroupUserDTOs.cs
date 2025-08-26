using QuemVaiVai.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.DTOs
{
    public class GroupUserDTO
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
    }
}
