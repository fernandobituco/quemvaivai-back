using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.Interfaces.Contexts;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/tasklists")]
    [ApiController]
    public class TaskListController : BaseController<TaskListController>
    {
        public TaskListController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<TaskListController> logger,
            IMapper mapper,
            IUserContext userContext) : base(httpContextAccessor, logger, mapper, userContext)
        {
        }


    }
}