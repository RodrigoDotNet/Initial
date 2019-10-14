using Initial.Api.Controllers;
using Initial.Api.Filters;
using Initial.Api.Models;
using Initial.Api.Tests.Controllers;
using Initial.Api.Tests.Util;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;

namespace Initial.Api.Tests.Filters
{
    [TestFixture]
    public class AuthorizeFilterTest 
        : ControllerDefaultBaseTest<CustomerController>
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void OnActionExecuting()
        {
            // Arrange

            var filter = new AuthorizeFilter();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };

            var actionExecutingContext =
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNull(actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_UnauthorizedResult()
        {
            // Arrange

            var filter = new AuthorizeFilter();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = null
            };

            var actionExecutingContext =
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNotNull(actionExecutingContext.Result);

            Assert.IsInstanceOf<UnauthorizedResult>
                (actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Area()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AccessAreaEnum.Customer, AccessModeEnum.Read);

            var access = new Models.Database.AreaAccess
            {
                AreaId = (int)AccessAreaEnum.Customer,
                CanRead = true
            };

            access.GroupId = Database.UserGroups
                .Where(e => e.UserId == AccountTicket.Id)
                .Select(e => e.GroupId)
                .First();

            Database.AreaAccess.Add(access);

            Database.SaveChanges();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };

            var actionExecutingContext =
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNull(actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Area_UnauthorizedResult()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AccessAreaEnum.Customer, AccessModeEnum.Create);

            var access = new Models.Database.AreaAccess
            {
                AreaId = (int)AccessAreaEnum.Customer,
                CanRead = true
            };

            access.GroupId = Database.UserGroups
                .Where(e => e.UserId == AccountTicket.Id)
                .Select(e => e.GroupId)
                .First();

            Database.AreaAccess.Add(access);

            Database.SaveChanges();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };

            var actionExecutingContext =
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNull(actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Policy()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AccessPolicyEnum.UserChangePassword);

            var access = new Models.Database.PolicyAccess
            {
                PolicyId = (int)AccessPolicyEnum.UserChangePassword
            };

            access.GroupId = Database.UserGroups
                .Where(e => e.UserId == AccountTicket.Id)
                .Select(e => e.GroupId)
                .First();

            Database.PolicyAccess.Add(access);

            Database.SaveChanges();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };

            var actionExecutingContext =
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNull(actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Policy_UnauthorizedResult()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AccessPolicyEnum.UserChangePassword);

            var access = new Models.Database.PolicyAccess
            {
                PolicyId = (int)AccessPolicyEnum.UserChangeEmail
            };

            access.GroupId = Database.UserGroups
                .Where(e => e.UserId == AccountTicket.Id)
                .Select(e => e.GroupId)
                .First();

            Database.PolicyAccess.Add(access);

            Database.SaveChanges();

            Controller = new CustomerController(null)
            {
                AccountService = AccountService,

                AccountTicket = AccountTicket
            };

            var actionExecutingContext = 
                MockHelper.ActionExecutingContext(Controller);

            // Act

            filter.OnActionExecuting(actionExecutingContext);

            // Assert

            Assert.IsNull(actionExecutingContext.Result);
        }

        #region Test

        [TestCase(AccessModeEnum.None, AccessModeEnum.None)]

        [TestCase(AccessModeEnum.Create, AccessModeEnum.Create)]
        [TestCase(AccessModeEnum.Delete, AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.Modify, AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Read, AccessModeEnum.Read)]

        [TestCase(AccessModeEnum.None, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Create, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Delete, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Modify, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Read, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.All, AccessModeEnum.All)]

        [TestCase(AccessModeEnum.Create | AccessModeEnum.Delete, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Delete | AccessModeEnum.Modify, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Modify | AccessModeEnum.Read, AccessModeEnum.All)]

        [TestCase(AccessModeEnum.Create | AccessModeEnum.Delete, AccessModeEnum.Create | AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.Delete | AccessModeEnum.Modify, AccessModeEnum.Delete | AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Modify | AccessModeEnum.Read, AccessModeEnum.Modify | AccessModeEnum.Read)]

        [TestCase(AccessModeEnum.Create | AccessModeEnum.Delete | AccessModeEnum.Modify, AccessModeEnum.All)]
        [TestCase(AccessModeEnum.Delete | AccessModeEnum.Modify | AccessModeEnum.Read, AccessModeEnum.All)]

        [TestCase(AccessModeEnum.Create | AccessModeEnum.Delete | AccessModeEnum.Modify, AccessModeEnum.Create | AccessModeEnum.Delete | AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Delete | AccessModeEnum.Modify | AccessModeEnum.Read, AccessModeEnum.Delete | AccessModeEnum.Modify | AccessModeEnum.Read)]
        public void Test_Equal(AccessModeEnum must, AccessModeEnum have)
        {
            // Arrange

            // Act

            var result = AuthorizeFilter.Test(must, have);

            // Assert

            Assert.IsTrue(result);
        }

        [TestCase(AccessModeEnum.Create, AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.Create, AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Create, AccessModeEnum.Read)]
        [TestCase(AccessModeEnum.Create, AccessModeEnum.None)]

        [TestCase(AccessModeEnum.Delete, AccessModeEnum.Create)]
        [TestCase(AccessModeEnum.Delete, AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Delete, AccessModeEnum.Read)]
        [TestCase(AccessModeEnum.Delete, AccessModeEnum.None)]

        [TestCase(AccessModeEnum.Modify, AccessModeEnum.Create)]
        [TestCase(AccessModeEnum.Modify, AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.Modify, AccessModeEnum.Read)]
        [TestCase(AccessModeEnum.Modify, AccessModeEnum.None)]

        [TestCase(AccessModeEnum.Read, AccessModeEnum.Create)]
        [TestCase(AccessModeEnum.Read, AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.Read, AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.Read, AccessModeEnum.None)]

        [TestCase(AccessModeEnum.All, AccessModeEnum.Create)]
        [TestCase(AccessModeEnum.All, AccessModeEnum.Delete)]
        [TestCase(AccessModeEnum.All, AccessModeEnum.Modify)]
        [TestCase(AccessModeEnum.All, AccessModeEnum.None)]
        public void Test_NotEqual(AccessModeEnum must, AccessModeEnum have)
        {
            // Arrange

            // Act

            var result = AuthorizeFilter.Test(must, have);

            // Assert

            Assert.IsFalse(result);
        }

        #endregion
    }
}
