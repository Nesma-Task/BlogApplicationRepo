using BloggingApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BloggingApplication.BLL
{
    public interface IBlogService
    {
        Task<User> GetUser(string userName, string password);
        Task<List<Post>> GetAllPosts();

        Task<int> CreatePost(Post post);
    }
}
