using QuemVaiVai.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IGroupAppService
    {
        Task<GroupDTO> GetById(int groupId, int userId);
        Task<GroupDTO> CreateGroupAsync(CreateGroupDTO request, int userId);
        Task<GroupDTO> UpdateGroupAsync(UpdateGroupDTO request, int userId);
        Task DeleteGroupAsync(int id, int userId);
    }
}
