using Initial.Api.Controllers;
using Initial.Api.Models;
using Initial.Api.Models.Templates;
using Initial.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Tests.Controllers
{
    [TestFixture]
    public class GroupControllerTest : ControllerServiceBaseTest
        <GroupRepository, GroupService, GroupController>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Repository = new GroupRepository(DbContext);

            Service = new GroupService(Repository, AppSettings);

            Controller = new GroupController(Service)
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

            Assert.IsInstanceOf<IEnumerable<GroupResponse>>(value);

            var response = (IEnumerable<GroupResponse>)value;

            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task GetAll_Authenticated()
        {
            // Arrange

            Controller
                .AccountTicket = null;

            // Act

            var actionResult = await Controller.GetAll();

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<IEnumerable<GroupResponse>>(value);

            var response = (IEnumerable<GroupResponse>)value;

            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task Get_Authenticated()
        {
            // Arrange

            var id = DbContext.Groups
                .First(e => e.EnterpriseId == AccountTicket.EnterpriseId).Id;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public async Task Get_ReadOnly()
        {
            // Arrange

            var id = DbContext.Groups
                .First(e => e.EnterpriseId == null).Id;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.AreEqual(id, response.Id);
        }

        [Test]
        public async Task Get_UnAuthenticated()
        {
            // Arrange

            Controller
                .AccountTicket = null;

            var id = DbContext.Groups
                .First(e => e.EnterpriseId == null).Id;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

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

            var request = new GroupRequest
            {
                Name = "Group B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

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

            var request = new GroupRequest
            {
                Name = "Group B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.Greater(response.Id, 0);

            Assert.AreEqual(request.Name, response.Name);

            // Arrange

            var id = response.Id;

            request.Name = "Group B+";

            // Act

            actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            okObjectResult = (OkObjectResult)actionResult;

            value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            response = (GroupResponse)value;

            Assert.AreEqual(id, response.Id);

            // Assert - Compare

            Assert.AreEqual(request.Name, response.Name);
        }

        [Test]
        public async Task Put_EntityVersion()
        {
            // Arrange

            var request = new GroupRequest
            {
                Name = "Group B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.Greater(response.Id, 0);

            Assert.AreEqual(request.Name, response.Name);

            // Arrange

            var id = response.Id;

            request.Name = "Group B+";

            request.EntityVersion = response.EntityVersion;

            // Act

            actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            okObjectResult = (OkObjectResult)actionResult;

            value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            response = (GroupResponse)value;

            Assert.AreEqual(id, response.Id);

            // Assert - Compare

            Assert.AreEqual(request.Name, response.Name);
        }

        [Test]
        public async Task Put_EntityVersion_Conflict()
        {
            // Arrange

            var request = new GroupRequest
            {
                Name = "Group B"
            };

            // Act

            var actionResult = await Controller.Post(request);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.Greater(response.Id, 0);

            Assert.AreEqual(request.Name, response.Name);

            // Arrange

            var id = response.Id;

            request.Name = "Group B+";

            request.EntityVersion = response.EntityVersion.Value.AddDays(-1);

            // Act

            actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<ConflictObjectResult>(actionResult);

            var conflictObjectResult = (ConflictObjectResult)actionResult;

            value = conflictObjectResult.Value;

            Assert.IsInstanceOf<StateResponse>(value);

            var stateResponse = (StateResponse)value;

            Assert.IsNotEmpty(stateResponse);

            Assert.IsTrue(stateResponse.ContainsKey("EntityVersion"));

            Assert.AreEqual(Messages.EntityVersion_Conflict, stateResponse["EntityVersion"]);
        }

        [Test]
        public async Task Put_EnterpriseId_ReadOnly()
        {
            // Arrange

            var id = DbContext.Groups
                .First(e => e.EnterpriseId == null).Id;

            // Act

            var actionResult = await Controller.Get(id);

            // Assert

            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var okObjectResult = (OkObjectResult)actionResult;

            var value = okObjectResult.Value;

            Assert.IsInstanceOf<GroupResponse>(value);

            var response = (GroupResponse)value;

            Assert.AreEqual(id, response.Id);


            // Arrange

            var request = new GroupRequest
            {
                Name = "Group B"
            };

            // Act

            actionResult = await Controller.Put(id, request);

            // Assert

            Assert.IsInstanceOf<ConflictObjectResult>(actionResult);

            var conflictObjectResult = (ConflictObjectResult)actionResult;

            value = conflictObjectResult.Value;

            Assert.IsInstanceOf<StateResponse>(value);

            var stateResponse = (StateResponse)value;

            Assert.IsNotEmpty(stateResponse);

            Assert.IsTrue(stateResponse.ContainsKey(""));

            Assert.AreEqual(Messages.EnterpriseId_ReadOnly, stateResponse[""]);
        }

        [Test]
        public async Task Put_NotFound()
        {
            // Arrange

            var id = 0;

            var request = new GroupRequest
            {
                Name = "Group B"
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