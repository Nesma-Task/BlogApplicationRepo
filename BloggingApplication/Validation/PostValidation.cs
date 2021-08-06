using BloggingApplication.API.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Validation
{

    public class PostValidation : AbstractValidator<PostModel>
    {
        public PostValidation()
        {
            RuleFor(r => r.PostContent).NotEmpty().WithMessage("Content is required")
           .Length(1, 140)
           .WithMessage("Content must be less than or equal to 140 characters");


        }
    }
}
