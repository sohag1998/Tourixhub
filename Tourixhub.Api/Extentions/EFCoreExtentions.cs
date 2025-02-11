using Tourixhub.Domain.Entities;
using Tourixhub.Domain;
using Microsoft.EntityFrameworkCore;
using Tourixhub.Infrastructure.Persistence;

namespace Tourixhub.Application.Extentions
{
    public static class EFCoreExtentions
    {
        public static IServiceCollection InjectDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            return services;
        }
    }
}
