﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Domain.Repository
{
    public interface IAppUserRepository: IRepository<AppUser, Guid>
    {
        Task<List<AppUser>> GetNonFriendUsersAsync(Guid currentUserId);
        Task<List<AppUser>> GetWhoSentRequest(Guid currentUserId);
    }
}
