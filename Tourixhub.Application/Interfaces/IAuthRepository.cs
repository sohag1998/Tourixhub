using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthResponseDto> Signup(AppUser user, string password);
        Task<AppUser?> IsAppUserExit(LoginDto loginDto);
    }
}
