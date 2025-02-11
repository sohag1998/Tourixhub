using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid AppUserId
        {
            get
            {
                var appUserId = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(appUserId))
                    throw new UnauthorizedAccessException("User not authenticated");
                return Guid.Parse(appUserId);
            }
        }
    }
}
