using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApplication.API.Model
{
    public class PostModel
    {

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the identifier for Post. </summary>
        ///
        /// <value> The identifier. </value>
        ///-------------------------------------------------------------------------------------------------
        public int Id { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets Post Title. </summary>
        ///
        /// <value> PostTitle. </value>
        ///-------------------------------------------------------------------------------------------------
        public string PostTitle { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets Post Content. </summary>
        ///
        /// <value> PostContent. </value>
        ///-------------------------------------------------------------------------------------------------
        public string PostContent { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets UserId Who write the post. </summary>
        ///
        /// <value> UserId . </value>
        ///-------------------------------------------------------------------------------------------------
        public int UserId { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets UserName Who write the post. </summary>
        ///
        /// <value> UserName . </value>
        ///-------------------------------------------------------------------------------------------------
        public string UserName { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets Image for the post. </summary>
        ///
        /// <value> Image . </value>
        ///-------------------------------------------------------------------------------------------------
        public string Image { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets CreatedDate. </summary>
        ///
        /// <value> CreatedDate . </value>
        ///-------------------------------------------------------------------------------------------------
        public DateTime CreatedDate { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets Latitude. </summary>
        ///
        /// <value> Latitude . </value>
        ///-------------------------------------------------------------------------------------------------
        public string Latitude { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets Longitude. </summary>
        ///
        /// <value> Longitude . </value>
        ///-------------------------------------------------------------------------------------------------
        public string Longitude { get; set; }
        public string Token { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile PostImage { get; set; }




    }

    


}
