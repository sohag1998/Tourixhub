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
    public class FriendService : IFriendService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IFriendHubService _friendHubService;

        public FriendService(
            IApplicationUnitOfWork applicationUnitOfWork, 
            IMapper mapper,
            IFriendHubService friendHubService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _friendHubService = friendHubService;
        }
        public async Task<bool> SendFriendRequestAsync(FriendRequestDto friendRequstDto, Guid senderId)
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

            await _friendHubService.SendFriendRequest(friendRequstDto.ReceiverId.ToString(), _mapper.Map<AppUserDto>(sender));

            return true;
        }

        

        public async Task<bool> DeclinedRequest(Guid currentUserId, Guid senderId)
        {
            var request = await _applicationUnitOfWork.FriendRequestReopsitory.GetFriendRequestAsync(currentUserId, senderId);
            if(request != null)
            {
                 _applicationUnitOfWork.FriendRequestReopsitory.Remove(request);
                await _applicationUnitOfWork.SaveAsync();
                var reciver = await _applicationUnitOfWork.AppUserRepository.GetByIdAsync(currentUserId);
                await _friendHubService.DeclinedFriendRequest(senderId.ToString(), currentUserId.ToString(), _mapper.Map<AppUserDto>(reciver));
                return true;
            }
            return false;


        }
        public async Task<bool> AcceptRequest(Guid currentUserId, Guid senderId)
        {
            var request = await _applicationUnitOfWork.FriendRequestReopsitory.GetFriendRequestAsync(currentUserId, senderId);
            var isFriendShipExist = await _applicationUnitOfWork.FriendRepository.IsFriendshipExist(currentUserId, senderId);
            if (request == null || isFriendShipExist) return false;

            request.Status = FriendRequestStatus.Accepted;

             _applicationUnitOfWork.FriendRequestReopsitory.Update(request);

            

            var newFriend = new Friendship
            {
                UserOneId = currentUserId,
                UserTwoId = senderId,
            };

            await _applicationUnitOfWork.FriendRepository.AddAsync(newFriend);

            await _applicationUnitOfWork.SaveAsync();
            var receiver = await _applicationUnitOfWork.AppUserRepository.GetByIdAsync(currentUserId);
            await _friendHubService.AcceptRequest(senderId.ToString(), _mapper.Map<AppUserDto>(receiver));

            return true;
        }

        public async Task<bool> RemoveFriend(Guid currentUserId, Guid otherUserId)
        {
            var requst = await _applicationUnitOfWork.FriendRequestReopsitory.GetFriendRequestAsync(currentUserId, otherUserId);

            var friendShip = await _applicationUnitOfWork.FriendRepository.GetFriendhipAsync(currentUserId, otherUserId);

            if(requst != null && friendShip != null)
            {
                _applicationUnitOfWork.FriendRequestReopsitory.Remove(requst);
                _applicationUnitOfWork.FriendRepository.Remove(friendShip);

                await _applicationUnitOfWork.SaveAsync();

                await _friendHubService.RemoveFreindshipAsync(currentUserId.ToString(), otherUserId.ToString());

                return true;
            }
            return false;
        }

        public async Task<List<AppUserDto>> GetReceivedReuests(Guid currentUserId)
        {
            var users = await _applicationUnitOfWork.FriendRequestReopsitory.GetReceivedRequests(currentUserId);

            return _mapper.Map<List<AppUserDto>>(users);
        }

        public async Task<List<AppUserDto>> GetAllFriends(Guid currentUserId)
        {

            var friends = await _applicationUnitOfWork.FriendRepository.GetAllFriends(currentUserId);

            return _mapper.Map<List<AppUserDto>>(friends);
        }

        public async Task<List<AppUserDto>> GetAllNonFriends(Guid currentUserId)
        {
            var nFriends = await _applicationUnitOfWork.FriendRepository.GetNonFriendUser(currentUserId);
            return _mapper.Map<List<AppUserDto>>(nFriends);
        }

    }
}
