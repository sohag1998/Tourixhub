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
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<AppUser?> IsAppUserExit(LoginDto loginDto)
        {
            return await _authRepository.IsAppUserExit(loginDto);
        }

        public async Task<AuthResponseDto> Signup(UserRegistrationDto user)
        {
            var appUser = _mapper.Map<AppUser>(user);
            appUser.UserName = appUser.Email;
            var result = await _authRepository.Signup(appUser, user.Password);

            return result;
        }

        public async Task<List<AppUser>> GellAllUserasync()
        {
            var users = await _authRepository.GellAllUserasync();
            return users;
        }
    }
}
