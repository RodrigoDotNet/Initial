using Initial.Api.Controllers;
using Initial.Api.Filters;
using Initial.Api.Models;
using Initial.Api.Models.Database;
using Initial.Api.Tests.Controllers;
using Initial.Api.Tests.Util;
using Initial.Api.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Initial.Api.Tests.Filters
{
    [TestFixture]
    public class AuthorizeFilterTest 
    {
        protected InitialDatabase DbContext { get; private set; }

        public AppSettings AppSettings { get; private set; }

        public AccountService AccountService { get; private set; }

        protected CustomerController Controller { get; set; }

        protected AccountTicket AccountTicket { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            var options = new DbContextOptionsBuilder<InitialDatabase>()
                .UseInMemoryDatabase(databaseName: "Initial_Api")
                .Options;

            DbContext = new InitialDatabase(options);

            InitialDatabaseInit.EnsureCreated = false;

            InitialDatabaseInit.Initialize(DbContext);

            AppSettings = AppSettings.Default;

            AccountService = new AccountService(new AccountRepository(DbContext), AppSettings);

            AccountService.IsValid(CryptoHelper.Guid("UP1"), out AccountTicket accountTicket);

            AccountTicket = accountTicket;
        }

        [TearDown]
        public virtual void Down()
        {
            DbContext.Database
                .EnsureDeleted();
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

        [TestCase(ModeEnum.All)]
        [TestCase(ModeEnum.Read)]
        [TestCase(ModeEnum.Create)]
        [TestCase(ModeEnum.Delete)]
        [TestCase(ModeEnum.Modify)]
        public void OnActionExecuting_Area(ModeEnum mode)
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AreaEnum.Customer, mode);

            var access = new Models.Database.AreaAccess
            {
                AreaId = (int)AreaEnum.Customer,
                Group = new Models.Database.Group
                {
                    Name = "Test",
                    UserGroups = new[]
                    {
                        new Models.Database.UserGroup
                        {
                            UserId = AccountTicket.Id
                        }
                    }
                },
                CanRead = mode.HasFlag(ModeEnum.Read),
                CanCreate = mode.HasFlag(ModeEnum.Create),
                CanModify = mode.HasFlag(ModeEnum.Modify),
                CanDelete = mode.HasFlag(ModeEnum.Delete)
            };

            DbContext.AreaAccess.Add(access);

            DbContext.SaveChanges();

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

        [TestCase(ModeEnum.All)]
        [TestCase(ModeEnum.Create)]
        [TestCase(ModeEnum.Delete)]
        [TestCase(ModeEnum.Modify)]
        public void OnActionExecuting_Area_UnauthorizedResult
            (ModeEnum mode)
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AreaEnum.Customer, mode);

            var access = new Models.Database.AreaAccess
            {
                AreaId = (int)AreaEnum.Customer,
                Group = new Models.Database.Group
                {
                    Name = "Test",
                    UserGroups = new[]
                    {
                        new Models.Database.UserGroup
                        {
                            UserId = AccountTicket.Id
                        }
                    }
                },
                CanRead = true
            };

            DbContext.AreaAccess.Add(access);

            DbContext.SaveChanges();

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

            Assert.IsNotNull(actionExecutingContext.Result);

            Assert.IsInstanceOf<UnauthorizedResult>
                (actionExecutingContext.Result);
        }

        [TestCase(ModeEnum.All)]
        [TestCase(ModeEnum.Read)]
        [TestCase(ModeEnum.Create)]
        [TestCase(ModeEnum.Delete)]
        [TestCase(ModeEnum.Modify)]
        public void OnActionExecuting_Area_UnauthorizedResult_None
            (ModeEnum mode)
        {
            // Arrange

            var filter = new AuthorizeFilter
                (AreaEnum.Customer, mode);

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

            Assert.IsNotNull(actionExecutingContext.Result);

            Assert.IsInstanceOf<UnauthorizedResult>
                (actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Policy()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (PolicyEnum.User_ChangeEmail);

            var access = new Models.Database.PolicyAccess
            {
                PolicyId = (int)PolicyEnum.User_ChangeEmail,
                Group = new Models.Database.Group
                {
                    Name = "Test",
                    UserGroups = new[]
                    {
                        new Models.Database.UserGroup
                        {
                            UserId = AccountTicket.Id
                        }
                    }
                }
            };

            DbContext.PolicyAccess.Add(access);

            DbContext.SaveChanges();

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
                (PolicyEnum.User_ChangePassword);

            var access = new Models.Database.PolicyAccess
            {
                PolicyId = (int)PolicyEnum.User_ChangeEmail,
                Group = new Models.Database.Group
                {
                    Name = "Test",
                    UserGroups = new[]
                    {
                        new Models.Database.UserGroup
                        {
                            UserId = AccountTicket.Id
                        }
                    }
                }
            };

            DbContext.PolicyAccess.Add(access);

            DbContext.SaveChanges();

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

            Assert.IsNotNull(actionExecutingContext.Result);

            Assert.IsInstanceOf<UnauthorizedResult>
                (actionExecutingContext.Result);
        }

        [Test]
        public void OnActionExecuting_Policy_UnauthorizedResult_None()
        {
            // Arrange

            var filter = new AuthorizeFilter
                (PolicyEnum.User_ChangePassword);

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

            Assert.IsNotNull(actionExecutingContext.Result);

            Assert.IsInstanceOf<UnauthorizedResult>
                (actionExecutingContext.Result);
        }

        #region Test

        [TestCase(ModeEnum.None, ModeEnum.None)]

        [TestCase(ModeEnum.Create, ModeEnum.Create)]
        [TestCase(ModeEnum.Delete, ModeEnum.Delete)]
        [TestCase(ModeEnum.Modify, ModeEnum.Modify)]
        [TestCase(ModeEnum.Read, ModeEnum.Read)]

        [TestCase(ModeEnum.None, ModeEnum.All)]
        [TestCase(ModeEnum.Create, ModeEnum.All)]
        [TestCase(ModeEnum.Delete, ModeEnum.All)]
        [TestCase(ModeEnum.Modify, ModeEnum.All)]
        [TestCase(ModeEnum.Read, ModeEnum.All)]
        [TestCase(ModeEnum.All, ModeEnum.All)]

        [TestCase(ModeEnum.Create | ModeEnum.Delete, ModeEnum.All)]
        [TestCase(ModeEnum.Delete | ModeEnum.Modify, ModeEnum.All)]
        [TestCase(ModeEnum.Modify | ModeEnum.Read, ModeEnum.All)]

        [TestCase(ModeEnum.Create | ModeEnum.Delete, ModeEnum.Create | ModeEnum.Delete)]
        [TestCase(ModeEnum.Delete | ModeEnum.Modify, ModeEnum.Delete | ModeEnum.Modify)]
        [TestCase(ModeEnum.Modify | ModeEnum.Read, ModeEnum.Modify | ModeEnum.Read)]

        [TestCase(ModeEnum.Create | ModeEnum.Delete | ModeEnum.Modify, ModeEnum.All)]
        [TestCase(ModeEnum.Delete | ModeEnum.Modify | ModeEnum.Read, ModeEnum.All)]

        [TestCase(ModeEnum.Create | ModeEnum.Delete | ModeEnum.Modify, ModeEnum.Create | ModeEnum.Delete | ModeEnum.Modify)]
        [TestCase(ModeEnum.Delete | ModeEnum.Modify | ModeEnum.Read, ModeEnum.Delete | ModeEnum.Modify | ModeEnum.Read)]
        public void Test_Equal(ModeEnum must, ModeEnum have)
        {
            // Arrange

            // Act

            var result = AuthorizeFilter.Test(must, have);

            // Assert

            Assert.IsTrue(result);
        }

        [TestCase(ModeEnum.Create, ModeEnum.Delete)]
        [TestCase(ModeEnum.Create, ModeEnum.Modify)]
        [TestCase(ModeEnum.Create, ModeEnum.Read)]
        [TestCase(ModeEnum.Create, ModeEnum.None)]

        [TestCase(ModeEnum.Delete, ModeEnum.Create)]
        [TestCase(ModeEnum.Delete, ModeEnum.Modify)]
        [TestCase(ModeEnum.Delete, ModeEnum.Read)]
        [TestCase(ModeEnum.Delete, ModeEnum.None)]

        [TestCase(ModeEnum.Modify, ModeEnum.Create)]
        [TestCase(ModeEnum.Modify, ModeEnum.Delete)]
        [TestCase(ModeEnum.Modify, ModeEnum.Read)]
        [TestCase(ModeEnum.Modify, ModeEnum.None)]

        [TestCase(ModeEnum.Read, ModeEnum.Create)]
        [TestCase(ModeEnum.Read, ModeEnum.Delete)]
        [TestCase(ModeEnum.Read, ModeEnum.Modify)]
        [TestCase(ModeEnum.Read, ModeEnum.None)]

        [TestCase(ModeEnum.All, ModeEnum.Create)]
        [TestCase(ModeEnum.All, ModeEnum.Delete)]
        [TestCase(ModeEnum.All, ModeEnum.Modify)]
        [TestCase(ModeEnum.All, ModeEnum.None)]
        public void Test_NotEqual(ModeEnum must, ModeEnum have)
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
