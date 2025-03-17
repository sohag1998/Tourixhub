using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
using Tourixhub.Domain.Dtos;
using Tourixhub.Domain.Entities;

namespace Tourixhub.Application.Mappings
{
    public class AutoProfile: Profile
    {
        public AutoProfile() 
        {
            // For AppUser
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, UserRegistrationDto>().ReverseMap();

            // For Post
            CreateMap<Post, AddPostDto>().ReverseMap();
            CreateMap<Comment, AddCommentDto>();

            CreateMap<AppUser, AppUserDto>().ReverseMap();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.FavoriteCount, opt => opt.MapFrom(src => src.Favorites.Count))
                .ForMember(dest => dest.ReportCount, opt => opt.MapFrom(src => src.Reports.Count))
                .ForMember(dest => dest.LikedByUserIds, opt => opt.MapFrom(src => src.Likes.Select(l => l.AppUserId)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
                .ForMember(dest => dest.FavoritedByUserIds, opt => opt.MapFrom(src => src.Favorites.Select(f => f.AppUserId)))
                .ForMember(dest => dest.ReportedByUserIds, opt => opt.MapFrom(src => src.Reports.Select(r => r.AppUserId)));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser));


            CreateMap<FriendRequest, AppUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>src.Sender.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Sender.FullName))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.Sender.ProfilePictureUrl));

            CreateMap<FriendDto, AppUserDto>();

            CreateMap<Chat, ChatDto>().ReverseMap();
              


        }
    }
}
