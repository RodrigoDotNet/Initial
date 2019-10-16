using Initial.Api.Controllers;
using Initial.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Tests.Controllers
{
    [TestFixture]
    public class AreaControllerTest : ControllerServiceBaseTest
        <AreaRepository, AreaService, AreaController>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Repository = new AreaRepository(DbContext);

            Service = new AreaService(Repository, AppSettings);

            Controller = new AreaController(Service);
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

            Assert.IsInstanceOf<IEnumerable<AreaResponse>>(value);

            var response = (IEnumerable<AreaResponse>)value;

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

            Assert.IsInstanceOf<AreaResponse>(value);

            var response = (AreaResponse)value;

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