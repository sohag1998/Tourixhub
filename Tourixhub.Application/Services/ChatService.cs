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

        public ChatService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
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

                return true;
            }
            catch
            {
                return false;
            }

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

        public async Task<ChatDto?> GetLastReceivedMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var message = await _applicationUnitOfWork.ChatRepository.GetLastReceivedMessageByAppUserId(currentUserId, senderId);

            return _mapper.Map<ChatDto>(message);
        }

        public async Task<ChatDto?> GetLastSendMessageByAppUserId(Guid currentUserId, Guid senderId)
        {
            var message = await _applicationUnitOfWork.ChatRepository.GetLastSendMessageByAppUserId(currentUserId, senderId);
            return _mapper.Map<ChatDto?>(message);
        }

    }
}
