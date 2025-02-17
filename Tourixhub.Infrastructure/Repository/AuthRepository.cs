using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Application.Interfaces;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Infrastructure.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public AuthRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> Signup(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = true,
                    Message = "User Created Successfully"
                };

            }
            else
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Errors = result.Errors.Select( e=> new ErrorDto
                    {
                        Code = e.Code,
                        Description = e.Description
                    })
                };
            }
            
        }
        public async Task<AppUser?> IsAppUserExit(LoginDto loginDto)
        {
            var appUser = await _userManager.FindByEmailAsync(loginDto.Email);

            if (appUser != null && await _userManager.CheckPasswordAsync(appUser, loginDto.Password))
            {
                return appUser;
            }
            else
            {
                return null;
            }
        }
    }
}
