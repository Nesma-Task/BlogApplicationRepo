using System;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BloggingApplication.API.Model;
using BloggingApplication.API.Validation;
using BloggingApplication.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BloggingApplication.BLL;

namespace BloggingApplication.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogUserPostController : ControllerBase
    {
       // public readonly BlogContext _blogContext;
        public readonly IValidator<PostModel> _postValidator;
        public readonly IMapper _mapper;
        public readonly IConfiguration _config;
        private IWebHostEnvironment _enviroment;
        public readonly IBlogService _blogService;
        public BlogUserPostController(
            IValidator<PostModel> postValidator
            , IMapper mapper, IConfiguration config, IWebHostEnvironment enviroment, IBlogService blogService)
        {
           
            _postValidator = postValidator;
            _mapper = mapper;
            _config = config;
            _enviroment = enviroment;
            _blogService = blogService;

        }
        

        [Route("[action]")]
        public async Task<ActionResult<PostModel>> GetAll()
        {

            var postList = await _blogService.GetAllPosts();

            var postListDto = _mapper.Map<List<PostModel>>(postList);

            return Ok(new BaseResponse() { Data = postListDto });


        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<PostModel>> Create([FromForm]PostModel postModel)
        {
            
            var result = _postValidator.Validate(postModel);
            if (result.IsValid)
            {
                // save image 
                if (postModel.PostImage != null)
                {
                    
                    string photoPath = "/Images/" + Guid.NewGuid() + Path.GetExtension(postModel.PostImage.FileName);
                    FileStorage.BlobStorageManager blobStorageManager = new FileStorage.BlobStorageManager();
                   string blobPath=  await blobStorageManager.UploadBlobImageAsync(postModel.PostImage.OpenReadStream());
                    
                    postModel.Image = blobPath;//photoPath;
                 
                }

                postModel.CreatedDate = DateTime.UtcNow;
                postModel.UserId = 1;
                var postObj = _mapper.Map<Post>(postModel);
                await _blogService.CreatePost(postObj);

                return Ok(new BaseResponse() { Data = postObj });

            }
            else
            {
                return Unauthorized(
                    new BaseResponse()
                    {
                        Message = string.Join(",", result.Errors.Select(er => er.ErrorMessage).ToArray()),
                        MessageCode = StatusCodes.Status406NotAcceptable,
                    });
            }
        }
        [NonAction]
        public string GenerateJsonWebToken(string userName, string userId)
        {



            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                new Claim(JwtRegisteredClaimNames.Sub,userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };



            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }

      


    }
}
