using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using backend.Controllers;
using backend.Models;
using backend.Repositories;
using Xunit;

//trial tests, still in development
namespace backend.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public void GetAll_WhenCalled_ReturnsAllUsers()
        {
          
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(new List<User>()); 

            var controller = new UserController(null, mockRepository.Object);

           
            var result = controller.Get() as OkObjectResult;

           
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result.Value);
            Assert.Equal(200, result.StatusCode);

        }

        [Fact]
        public void Get_Returns_Single_User_By_Id()
        {
  
            int userId = 1;
            var user = new User(userId, "TestUser", "Password123");
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(repo => repo.GetById(userId)).Returns(user);

            var controller = new UserController(null, mockRepository.Object);

            var result = controller.Get(userId) as OkObjectResult;


            Assert.NotNull(result);
            Assert.IsType<User>(result.Value);
            Assert.Equal(200, result.StatusCode);

        }
        [Fact]
        public void Post_Returns_OkResult_On_Successful_Add()
        {
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<User>())).Returns(true);

            var user = new User(userId: 1, username: "testUser", password: "testPassword");
            var controller = new UserController(null, mockRepository.Object);

            var result = controller.Post(user) as OkResult;


            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Put_Returns_OkResult_On_Successful_Update()
        {
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(repo => repo.Update(It.IsAny<User>())).Returns(true);

            var user = new User(userId: 1, username: "testUser", password: "testPassword");
            var controller = new UserController(null, mockRepository.Object);

            var result = controller.Put(user) as OkResult;


            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Delete_Returns_OkResult_On_Successful_Deletion()
        {
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);

            var controller = new UserController(null, mockRepository.Object);

            var result = controller.Delete(1) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


    }
}
