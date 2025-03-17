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
    public class ChatRepository: Repository<Chat, Guid>, IChatRepository
    {
        private readonly ApplicationDbContext _context;
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Chat>> GetAllReceivedMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var chats = await _context.Chats
                .Where(m => m.ReceiverId == currentUserId && m.SenderId == senderId)
                .ToListAsync();

            return chats;
        }

        public async Task<List<Chat>> GetAllSendMessageByAppUserId(Guid currentUseId, Guid receiverId)
        {
            var chats = await _context.Chats
                .Where(m => m.SenderId == currentUseId && m.ReceiverId == receiverId)
                .ToListAsync();

            return chats;
        }

        public async Task<Chat?> GetLastReceivedMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var message = await _context.Chats
                .Where(m => m.ReceiverId == currentUserId && m.SenderId == senderId)
                .OrderByDescending(m => m.CreateAt)
                .FirstOrDefaultAsync();

            return message;
        }

        public async Task<Chat?> GetLastSendMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var message = await _context.Chats
                .Where(m => m.ReceiverId == senderId && m.SenderId == currentUserId)
                .OrderByDescending(m => m.CreateAt)
                .FirstOrDefaultAsync();

            return message;
        }
    }
}
