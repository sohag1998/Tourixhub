using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IFriendRepository: IRepository<Friendship, Guid>
    {
        Task<bool> IsFriendshipExist(Guid userOneId, Guid userTwoId);
        Task<List<FriendDto>> GetAllFriends(Guid currentUserId);
        Task<List<FriendDto>> GetNonFriendUser(Guid currentUserId);

        Task<Friendship?> GetFriendhipAsync(Guid userOneId, Guid userTwoId);
    }
}
