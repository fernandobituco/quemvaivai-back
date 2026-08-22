using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Domain.Exceptions;

namespace QuemVaiVai.Api.Controllers
{
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;
        protected readonly IUserContext _userContext;
        protected BaseController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<T> logger,
            IMapper mapper,
            IUserContext userContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
            _userContext = userContext;
        }

        protected void ModelStateValidation()
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState
                    .Where(kvp => kvp.Value?.Errors.Count > 0)
                    .Select(kvp => new
                    {
                        Campo = kvp.Key,
                        Mensagem = kvp.Value?.Errors.First().ErrorMessage
                    })
                    .FirstOrDefault();

                throw new InvalidModelStateException($"Campo inválido: {firstError?.Campo} - {firstError?.Mensagem}");
            }
        }

        protected string? GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refreshToken"];
        }
    }
}