using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IChatRepository: IRepository<Chat, Guid>
    {
        Task<List<Chat>> GetAllReceivedMessageByAppUserId(Guid currentUserId, Guid senderId);
        Task<List<Chat>> GetAllSendMessageByAppUserId(Guid currentUseId, Guid receiverId);

        Task<Chat?> GetLastReceivedMessageByAppUserId(Guid currentUserId, Guid senderId);
        Task<Chat?> GetLastSendMessageByAppUserId(Guid currentUserId, Guid senderId);
    }
}
