using System;
using System.Security.Claims;

namespace eHotels.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;


        }

        public string getUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

