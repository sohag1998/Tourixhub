using Tourixhub.Domain.Entities;
using Tourixhub.Application.Extentions;
using Tourixhub.Application.Interfaces;
using Tourixhub.Infrastructure.Persistence;
using Tourixhub.Infrastructure.Repository;
using Tourixhub.Application.Services;
using Tourixhub.Application.Mappings;
using Tourixhub.Domain.Repository;
using System.Text.Json.Serialization;
using Tourixhub.Infrastructure.SignalR;
using Microsoft.Extensions.Primitives;

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
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILikeHubService, LikeHubService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();


// AutoMapper Profiler
builder.Services.AddAutoMapper(typeof(AutoProfile));


builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:4200")  // Angular App URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());  // Important for SignalR to include cookies
});


builder.Services.AddSignalR();

builder.Services.AddControllers();



var app = builder.Build();

// Use CORS policy globally
app.UseCors("AllowLocalhost");

app.ConfigureSwaggerExplorer()
   .AddIdentityAuthMiddleware();



app.UseHttpsRedirection();

app.MapControllers();
//app.MapIdentityApi<AppUser>();
app.MapHub<LikeHub>("/likeHub");
    

app.Run();
