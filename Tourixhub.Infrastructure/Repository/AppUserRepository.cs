using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Domain.Entities;
using Tourixhub.Domain.Repository;
using Tourixhub.Infrastructure.Persistence;

namespace Tourixhub.Infrastructure.Repository
{
    public class AppUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppUserRepository(ApplicationDbContext context) 
        {
            _dbContext = context;
        }
        public void Add(AppUser appUser)
        {
            _dbContext.AppUsers.Add(appUser);
        }
        public async Task<List<AppUser>> GetLatestUsersAsync()
        {
            return await _dbContext.AppUsers.ToListAsync();
        }
    }
}
