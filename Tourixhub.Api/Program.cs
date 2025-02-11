using Tourixhub.Domain.Entities;
using Tourixhub.Application.Extentions;
using Tourixhub.Application.Interfaces;
using Tourixhub.Infrastructure.Persistence;
using Tourixhub.Infrastructure.Repository;
using Tourixhub.Application.Services;
using Tourixhub.Application.Mappings;
using Tourixhub.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApiService()
                .InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration)
                .AddIdentityHandlerAndStores()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

// All Custom Service
builder.Services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserContextService, UserContextService>();


// AutoMapper Profiler
builder.Services.AddAutoMapper(typeof(AutoProfile));


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();


var app = builder.Build();

app.ConfigureSwaggerExplorer()
   .ConfigureCors(builder.Configuration)
   .AddIdentityAuthMiddleware();


app.UseHttpsRedirection();

app.MapControllers();
//app.MapIdentityApi<AppUser>();

app.Run();
