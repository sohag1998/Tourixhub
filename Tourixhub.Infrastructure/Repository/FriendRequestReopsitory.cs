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
    public class FriendRequestReopsitory : Repository<FriendRequest, Guid>, IFriendRequestReopsitory 
    {
        private readonly ApplicationDbContext _dbContext;
        public FriendRequestReopsitory(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<FriendRequest?> GetFriendRequestAsync(Guid senderId, Guid receiverId)
        {
            var request =  await _dbContext.FriendRequests.FirstOrDefaultAsync(fr => 
            (fr.SenderId == senderId && fr.ReceiverId == receiverId) || 
            (fr.SenderId == receiverId && fr.ReceiverId == senderId));

            if (request != null) return request;
            return null;
        }

        public async Task<List<FriendRequest>> GetReceivedRequests(Guid currentUserId)
        {
            var receivedRequest = await _dbContext.FriendRequests
                .Where(r => r.ReceiverId  == currentUserId && r.Status == FriendRequestStatus.Pending)
                .Include(r => r.Sender)
                .ToListAsync();

            return receivedRequest;
        }

    }
}
