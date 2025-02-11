using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourixhub.Application.Dtos;
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
        }
    }
}
