using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;

namespace Tourixhub.Application.Interfaces
{
    public interface IFriendService
    {
        Task<bool> SendFriendRequestAsync(FriendRequstDto friendRequstDto, Guid senderId);
    }
}
