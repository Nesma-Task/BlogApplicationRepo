using BloggingApplication.BLL;
using Moq;
using System;
using Xunit;

namespace BloggingApplication.XUnit
{
    public class UnitTest1
    {
        #region Property  
        public Mock<IBlogService> mock = new Mock<IBlogService>();
        #endregion
        [Fact]
        public void Test1()
        {

        }
        //[Fact]
        //public async void GetUser(string userName, string password)
        //{
        //    mock.Setup(p => p.GetUser(userName, password)).ReturnsAsync("JK");
        //    BlogService emp = new BlogService(mock.Object);
        //    string result = await emp.GetEmployeeById(1);
        //    Assert.Equal("JK", result);
        //}
    }
    
}
