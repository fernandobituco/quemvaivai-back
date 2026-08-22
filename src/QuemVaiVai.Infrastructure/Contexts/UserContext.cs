using Microsoft.AspNetCore.Http;
using QuemVaiVai.Application.Interfaces.Contexts;
using System.Security.Claims;

namespace QuemVaiVai.Infrastructure.Contexts
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var authorization =
                httpContext?.Request.Headers["Authorization"]
                    .FirstOrDefault();

            Console.WriteLine(
                $"Authorization recebido pelo backend: {authorization}"
            );

            var user = httpContext?.User;

            Console.WriteLine(
                $"IsAuthenticated: {user?.Identity?.IsAuthenticated}"
            );

            Console.WriteLine(
                $"AuthenticationType: {user?.Identity?.AuthenticationType}"
            );

            Console.WriteLine(
                $"Claims: {string.Join(" | ", user?.Claims.Select(c => $"{c.Type}={c.Value}") ?? [])}"
            );

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }
            }

            return null;
        }
    }
}
