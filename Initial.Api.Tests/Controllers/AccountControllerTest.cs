using Initial.Api.Controllers;
using Initial.Api.Models;
using Initial.Api.Util;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Initial.Api.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest : ControllerServiceBaseTest
        <AccountRepository, AccountService, AccountController>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Repository = new AccountRepository(DbContext);

            Service = new AccountService(Repository, AppSettings);

            Controller = new AccountController(Service);
        }

        [Test]
        public async Task Login()
        {
            // Arrange

            var request = new AccountLoginRequest
            {
                Email = "user@enterprisetest.com",
                Password = "user2019"
            };

            // Act

            var actionResult = await Controller.Login(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<AccountLoginResponse>(value);

            var response = (AccountLoginResponse)value;

            Assert.IsNotNull(response);

            Assert.AreEqual(request.Email, response.Email);

            Assert.AreEqual(1, response.Id);

            Assert.AreEqual("User", response.Name);

            Assert.AreEqual(CryptoHelper.Guid("U$1"), response.PublicId);

            Assert.IsNotNull(response.Token);
        }

        [Test]
        public async Task Login_WrongEmail()
        {
            // Arrange

            var request = new AccountLoginRequest
            {
                Email = "user@enterprisetest.com.br",
                Password = "user2019"
            };

            // Act

            var actionResult = await Controller.Login(request);

            // Assert

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [Test]
        public async Task Login_WrongPassword()
        {
            // Arrange

            var request = new AccountLoginRequest
            {
                Email = "user@enterprisetest.com",
                Password = "user2018"
            };

            // Act

            var actionResult = await Controller.Login(request);

            // Assert

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }
}