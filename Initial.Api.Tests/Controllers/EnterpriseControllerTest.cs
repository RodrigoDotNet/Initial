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

            Repository = new EnterpriseRepository(Database);

            Service = new EnterpriseService(Repository);

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

            var id = 1;

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

        [Test]
        public async Task Post()
        {
            // Arrange

            var request = new EnterpriseRequest
            {
                Name = "Enterprise B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<EnterpriseResponse>(value);

            var response = (EnterpriseResponse)value;

            Assert.Greater(response.Id, 0);

            Assert.AreEqual(request.Name, response.Name);
        }

        [Test]
        public async Task Post_BadRequest()
        {
            // Arrange

            // Act

            var actionResult = await Controller.Post(null);

            // Assert

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }

        [Test]
        public async Task Put()
        {
            // Arrange

            var request = new EnterpriseRequest
            {
                Name = "Enterprise B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<EnterpriseResponse>(value);

            var response = (EnterpriseResponse)value;

            Assert.Greater(response.Id, 0);

            Assert.AreEqual(request.Name, response.Name);

            // Arrange

            var id = response.Id;

            request.Name = "Enterprise B+";

            // Act

            actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            okObjectResult = (OkObjectResult)actionResult;

            value = okObjectResult.Value;

            Assert.IsInstanceOf<EnterpriseResponse>(value);

            response = (EnterpriseResponse)value;

            Assert.AreEqual(id, response.Id);

            // Assert - Compare

            Assert.AreEqual(request.Name, response.Name);
        }

        [Test]
        public async Task Put_NotFound()
        {
            // Arrange

            var id = 0;

            var request = new EnterpriseRequest
            {
                Name = "Enterprise B"
            };

            // Act

            var actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [Test]
        public async Task Put_BadRequest()
        {
            // Arrange

            var id = 0;

            // Act

            var actionResult = await Controller.Put(id, null);

            // Assert

            Assert.IsInstanceOf<BadRequestResult>(actionResult);
        }
    }
}