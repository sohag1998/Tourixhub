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
    public class PostImageRepository : Repository<PostImage, Guid>, IPostImageRepository
    {
        public PostImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
