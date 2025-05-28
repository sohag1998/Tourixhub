using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;

namespace Tourixhub.Application.Interfaces
{
    public interface IChatService
    {
        Task<bool> AddMessage(Guid currentUseId, AddMessageDto message);

        Task<List<ChatDto2>> GetMessages(Guid currentUserId, Guid senderId);
        Task<List<ChatDto>> GetAllReceivedMessageByAppUserId(Guid currentUserId, Guid senderId);
        Task<List<ChatDto>> GetAllSendMessageByAppUserId(Guid currentUseId, Guid receiverId);
        Task<ChatDto2?> GetLastReceivedMessageByAppUserId(Guid currentUserId, Guid senderId);
        Task<ChatDto2?> GetLastSendMessageByAppUserId(Guid currentUserId, Guid receiverId);
    }
}
