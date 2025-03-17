using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;
using Tourixhub.Domain.Repository;
using Tourixhub.Infrastructure.Persistence;

namespace Tourixhub.Infrastructure.Repository
{
    public class AppUserRepository: Repository<AppUser, Guid>, IAppUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppUserRepository(ApplicationDbContext context): base(context) 
        {
            _dbContext = context;
        }

        public async Task<List<AppUser>> GetNonFriendUsersAsync(Guid currentUserId)
        {
            var friends = await _dbContext.Friends
                .Where(f => f.UserOneId == currentUserId || f.UserTwoId == currentUserId)
                .Select(f => f.UserOneId == currentUserId ? f.UserTwoId : f.UserOneId)
                .ToListAsync();

            var pendingRequest = await _dbContext.FriendRequests
                .Where(fr => (fr.SenderId == currentUserId || fr.ReceiverId == currentUserId))
                .Select(fr => fr.SenderId == currentUserId ? fr.ReceiverId : fr.SenderId)
                .ToListAsync();

            var nonFriendsUser = await _dbContext.AppUsers
                .Where(u => u.Id != currentUserId &&
                !friends.Contains(u.Id) &&
                !pendingRequest.Contains(u.Id))
                .ToListAsync();

            return nonFriendsUser;
        }

        public async Task<List<AppUser>> GetWhoSentRequest(Guid currentUserId)
        {
            var users = await _dbContext.AppUsers
                .Where(u => u.SentRequests.Any(r => r.ReceiverId == currentUserId))
                .ToListAsync();
            
            return users;
        }
    }
}
