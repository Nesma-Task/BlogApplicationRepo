using System;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BloggingApplication.API.Model;

using BloggingApplication.Core.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BloggingApplication.BLL;

namespace BloggingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
    
  
        public readonly IMapper _mapper;
        public readonly IConfiguration _config;
        public readonly IBlogService _blogService;
        public UserController(
           
             IMapper mapper, IConfiguration config, IBlogService blogService)
        {
         
            _mapper = mapper;
            _config = config;
            _blogService = blogService;

        }
        [Route("[action]")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserModel>> Login(string userName,string password)
        {

           
            var existUser= await _blogService.GetUser(userName, password);
                if (existUser != null)
                {
                    var usermodel = _mapper.Map<UserModel>(existUser);
                    usermodel.Token = GenerateJsonWebToken(usermodel.UserName, usermodel.Id.ToString());
                    return Ok(new BaseResponse() { Data = usermodel });
                }
                else
                {
                  return Unauthorized(
                    new BaseResponse()
                    {
                        Message = "Invalid username or password",
                        MessageCode = StatusCodes.Status203NonAuthoritative,
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
