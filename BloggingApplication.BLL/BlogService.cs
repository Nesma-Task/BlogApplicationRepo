using BloggingApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.BLL
{
    public class BlogService:IBlogService
    {
        public readonly BlogContext _blogContext;
      
        //Constructore
        public BlogService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public Task< User> GetUser(string userName, string password)
        {
            var existUser= _blogContext.Users.FirstOrDefaultAsync(us => us.UserName == userName && us.Password == password);
            if (existUser != null)
            {
                return existUser;
            }
            else
            {
                return null;
            }

        }

        public Task<List<Post>> GetAllPosts()
        {
         return _blogContext.Posts.OrderByDescending(sa => sa.CreatedDate).ToListAsync();
           
        }
        public Task<int> CreatePost(Post post)
        {
            _blogContext.Posts.Add(post);
          return  _blogContext.SaveChangesAsync();
        }


    }
}
