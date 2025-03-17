using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;

namespace Tourixhub.Application.Interfaces
{
    public interface IFriendHubService
    {
        Task SendFriendRequest(string receiverId, AppUserDto appUserDto);
        Task DeclinedFriendRequest(string senderId, string receiverId, AppUserDto receiver);
        Task AcceptRequest(string senderId, AppUserDto receiver);
        Task RemoveFreindshipAsync(string currentUserId, string userTwoId);
    }
}
