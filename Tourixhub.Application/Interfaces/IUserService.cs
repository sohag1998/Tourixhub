using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<AppUserDto>> GetNonFriendUsersAsync(Guid currentUserId);
        Task<List<AppUserDto>> GetWhoSentRequest(Guid currentUserId);
    }
}
