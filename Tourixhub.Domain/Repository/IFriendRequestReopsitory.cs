using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IFriendRequestReopsitory: IRepository<FriendRequest, Guid>
    {
        Task<FriendRequest?> GetFriendRequestAsync(Guid senderId, Guid receiverId);
        Task<List<FriendRequest>> GetReceivedRequests(Guid currentUserId);
    }
}
