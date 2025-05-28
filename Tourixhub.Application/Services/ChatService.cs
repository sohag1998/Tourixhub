using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Services
{
    public class ChatService: IChatService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IChatHubService _chatHubService;

        public ChatService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper, IChatHubService chatHubService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _chatHubService = chatHubService;
        }

        public async Task<bool> AddMessage(Guid currentUseId, AddMessageDto message)
        {
            try
            {
                if (message == null) return false;

                var newMessage = new Chat
                {
                    ReceiverId = message.ReceiverId,
                    SenderId = currentUseId,
                    Message = message.Message,
                };

                await _applicationUnitOfWork.ChatRepository.AddAsync(newMessage);
                await _applicationUnitOfWork.SaveAsync();
                var message2 = await GetLastSendMessageByAppUserId(currentUseId, message.ReceiverId);

                if(message2 != null)
                    await _chatHubService.UpdateSendMessage(message.ReceiverId.ToString(), message2);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<List<ChatDto2>> GetMessages(Guid currentUserId, Guid senderId)
        {
            var messages = await _applicationUnitOfWork.ChatRepository.GetMessages(currentUserId, senderId);

            return _mapper.Map<List<ChatDto2>>(messages);
        }
        public async Task<List<ChatDto>> GetAllReceivedMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var messages = await _applicationUnitOfWork.ChatRepository.GetAllReceivedMessageByAppUserId(currentUserId, senderId);

            return _mapper.Map<List<ChatDto>>(messages);
        }
        public async Task<List<ChatDto>> GetAllSendMessageByAppUserId(Guid currentUseId, Guid receiverId)
        {
            var messages = await _applicationUnitOfWork.ChatRepository.GetAllSendMessageByAppUserId(currentUseId, receiverId);

            return _mapper.Map<List<ChatDto>>(messages);
        }

        public async Task<ChatDto2?> GetLastReceivedMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var message = await _applicationUnitOfWork.ChatRepository.GetLastReceivedMessageByAppUserId(currentUserId, senderId);

            return _mapper.Map<ChatDto2>(message);
        }

        public async Task<ChatDto2?> GetLastSendMessageByAppUserId(Guid currentUserId, Guid receiverId)
        {
            var message = await _applicationUnitOfWork.ChatRepository.GetLastSendMessageByAppUserId(currentUserId, receiverId);
            return _mapper.Map<ChatDto2?>(message);
        }

    }
}
