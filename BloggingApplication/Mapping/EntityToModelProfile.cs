using AutoMapper;
using BloggingApplication.API.Model;
using BloggingApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Mapping
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<Post, PostModel>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(o => o.User.UserName));

            //CreateMap<LoginModel, User>();
            CreateMap<PostModel, Post>();
        }
    }
}
