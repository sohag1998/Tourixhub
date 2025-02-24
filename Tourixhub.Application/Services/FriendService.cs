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
    public class FriendService : IFriendService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        public FriendService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task<bool> SendFriendRequestAsync(FriendRequstDto friendRequstDto, Guid senderId)
        {
            var exstingRequest = await _applicationUnitOfWork.FriendRequestReopsitory.GetFriendRequestAsync(senderId, friendRequstDto.ReceiverId);
            var sender = await _applicationUnitOfWork.AppUserRepository.GetByIdAsync(senderId);
            var receiver = await _applicationUnitOfWork.AppUserRepository.GetByIdAsync(friendRequstDto.ReceiverId);

           if(exstingRequest != null || exstingRequest?.Status == FriendRequestStatus.Accepted || sender == null || receiver == null)
            {
                return false;
            }

            var newRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = friendRequstDto.ReceiverId,
                Status = FriendRequestStatus.Pending

            };

            await _applicationUnitOfWork.FriendRequestReopsitory.AddAsync(newRequest);
            await _applicationUnitOfWork.SaveAsync();

            return true;
        }
    }
}
