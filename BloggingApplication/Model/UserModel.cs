using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

    }
    public class LoginModel
    {
      
        public string userName { get; set; }
        public string password { get; set; }

    }
}
