using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Dtos;
using Tourixhub.Domain.Entities;
using Tourixhub.Domain.Repository;
using Tourixhub.Infrastructure.Persistence;

namespace Tourixhub.Infrastructure.Repository
{
    public class FriendRepository: Repository<Friendship, Guid>, IFriendRepository
    {
       private readonly ApplicationDbContext _dbContext;
        public FriendRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsFriendshipExist(Guid userOneId, Guid userTwoId)
        {
            var friendship = await _dbContext.Friends
                .FirstOrDefaultAsync(fr => 
                (fr.UserOneId == userOneId && fr.UserTwoId == userTwoId) || 
                (fr.UserTwoId == userOneId && fr.UserOneId == userTwoId));
            if (friendship == null) return false;
            return true;
        }

        public async Task<Friendship?> GetFriendhipAsync(Guid userOneId, Guid userTwoId)
        {
            var friendship = await _dbContext.Friends
                .FirstOrDefaultAsync(fr =>
                (fr.UserOneId == userOneId && fr.UserTwoId == userTwoId) ||
                (fr.UserTwoId == userOneId && fr.UserOneId == userTwoId));
            if (friendship == null) return null;
            return friendship;
        }


        public async Task<List<FriendDto>> GetAllFriends(Guid currentUserId)
        {
            var friends = await _dbContext.Friends
                .Where(f => f.UserOneId == currentUserId || f.UserTwoId == currentUserId)
                .Select(f => new FriendDto
                {
                    Id = f.UserOneId == currentUserId ? f.UserTwoId : f.UserOneId,
                    FullName = f.UserOneId == currentUserId ? f.UserTwo.FullName : f.UserOne.FullName,
                    ProfilePictureUrl = f.UserOneId == currentUserId ? f.UserTwo.ProfilePictureUrl : f.UserOne.ProfilePictureUrl
                }).ToListAsync();

            return friends;
        }

        public async Task<List<FriendDto>> GetNonFriendUser(Guid currentUserId)
        {
            var nfriends = await _dbContext.AppUsers
                .Where(u => u.Id != currentUserId && 
                    !_dbContext.FriendRequests
                    .Where(fr => fr.SenderId == currentUserId || fr.ReceiverId == currentUserId)
                    .Select(fr => fr.SenderId == currentUserId ? fr.ReceiverId : fr.SenderId)
                    .Contains(u.Id) && 
                    !_dbContext.Friends
                    .Where(f => f.UserOneId == currentUserId || f.UserTwoId == currentUserId)
                    .Select(f => f.UserOneId == currentUserId ? f.UserTwoId : f.UserOneId)
                    .Contains(u.Id)
                
                )
                .Select(u => new FriendDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    ProfilePictureUrl = u.ProfilePictureUrl
                }
                    
                ).ToListAsync();

            return nfriends;
        }
    }
}
