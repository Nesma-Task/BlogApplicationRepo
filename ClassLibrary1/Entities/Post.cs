using System;
using System.Collections.Generic;

#nullable disable

namespace BloggingApplication.Core.Entities
{
    public partial class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
