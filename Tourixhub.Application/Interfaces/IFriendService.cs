using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Interfaces
{
    public interface IFriendService
    {
        Task<bool> SendFriendRequestAsync(FriendRequestDto friendRequstDto, Guid senderId);
        Task<bool> DeclinedRequest(Guid currentUserId, Guid senderId);
        Task<List<AppUserDto>> GetReceivedReuests(Guid currentUserId);
        Task<bool> AcceptRequest(Guid currentUserId, Guid senderId);
        Task<List<AppUserDto>> GetAllFriends(Guid currentUserId);
        Task<List<AppUserDto>> GetAllNonFriends(Guid currentUserId);
        Task<bool> RemoveFriend(Guid currentUserId, Guid otherUserId);
    }
}
