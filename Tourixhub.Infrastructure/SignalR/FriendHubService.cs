using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Infrastructure.SignalR
{
    public class FriendHubService: IFriendHubService
    {
        public IHubContext<ApplicationHub> _context;

        public FriendHubService(IHubContext<ApplicationHub> context)
        {
            _context = context;
        }

        public async Task SendFriendRequest(string receiverId, AppUserDto sender)
        {
            var connectionId = ApplicationHub.GetConnectionId(receiverId);
            if(connectionId != null)
            {
                await _context.Clients.Client(connectionId).SendAsync("ReceivedFriendRequest", receiverId, sender);
            }
           
        }

        public async Task DeclinedFriendRequest(string senderId, string receiverId, AppUserDto receiver)
        {
            var connectionId = ApplicationHub.GetConnectionId(senderId);
            if( connectionId != null)
            {
                await _context.Clients.Client(connectionId).SendAsync("ReceivedDeclinedRequest", receiverId, receiver);
            }
        }

        public async Task AcceptRequest(string senderId, AppUserDto receiver)
        {
            var connectionId = ApplicationHub.GetConnectionId(senderId);
            if(connectionId != null)
            {
                await _context.Clients.Client(connectionId).SendAsync("ReceivedAcceptFriendRequest", receiver);
            }
        }

        public async Task RemoveFreindshipAsync(string currentUserId, string userTwoId)
        {
            var connectionId = ApplicationHub.GetConnectionId(userTwoId);
            if(connectionId != null)
            {
                await _context.Clients.Client(connectionId).SendAsync("UpdateFriendAfterRemove", currentUserId);
            }
        }
    }
}
