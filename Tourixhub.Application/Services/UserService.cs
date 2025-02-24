﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;

namespace Tourixhub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        public UserService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AppUserDto>> GetNonFriendUsersAsync(Guid currentUserId)
        {
            var nonFriendUsers = await _applicationUnitOfWork.AppUserRepository.GetNonFriendUsersAsync(currentUserId);

            var newUsers = _mapper.Map<List<AppUserDto>>(nonFriendUsers);
            return newUsers;

        }
    }
}
