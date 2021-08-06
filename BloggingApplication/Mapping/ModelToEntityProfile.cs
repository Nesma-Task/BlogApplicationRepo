using AutoMapper;
using BloggingApplication.API.Model;
using BloggingApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            //CreateMap<LoginModel, User>();
            CreateMap<PostModel, Post>();
        }
    }

}
