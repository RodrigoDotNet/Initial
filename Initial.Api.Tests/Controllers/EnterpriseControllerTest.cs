using Initial.Api.Controllers;
using Initial.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Tests.Controllers
{
    [TestFixture]
    public class EnterpriseControllerTest : ControllerServiceBaseTest
        <EnterpriseRepository, EnterpriseService, EnterpriseController>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Repository = new EnterpriseRepository(DbContext);

            Service = new EnterpriseService(Repository, AppSettings);

            Controller = new EnterpriseController(Service)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };
        }

        [Test]
        public async Task GetAll()
        {
            // Arrange

            // Act

            var actionResult = await Controller.GetAll();

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<IEnumerable<EnterpriseResponse>>(value);

            var response = (IEnumerable<EnterpriseResponse>)value;

            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task Get()
        {
            // Arrange

            var id = AccountTicket.EnterpriseId;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<EnterpriseResponse>(value);

            var response = (EnterpriseResponse)value;

            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public async Task Get_NotFound()
        {
            // Arrange

            var id = 0;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }
}