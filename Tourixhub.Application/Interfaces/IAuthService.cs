using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Signup(UserRegistrationDto user);
        Task<AppUser?> IsAppUserExit(LoginDto loginDto);
        Task<List<AppUser>> GellAllUserasync();
    }
}
