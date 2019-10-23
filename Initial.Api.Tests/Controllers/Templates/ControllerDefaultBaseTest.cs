using Initial.Api.Models;
using Initial.Api.Models.Database;
using Initial.Api.Util;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Initial.Api.Tests.Controllers
{
    public abstract class ControllerDefaultBaseTest
        <TController>
    {
        protected InitialDatabase DbContext { get; private set; }

        public AppSettings AppSettings { get; private set; }

        public AccountService AccountService { get; private set; }

        protected TController Controller { get; set; }

        protected AccountTicket AccountTicket { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            var options = new DbContextOptionsBuilder<InitialDatabase>()
                .UseInMemoryDatabase(databaseName: "Initial_Api")
                .Options;

            DbContext = new InitialDatabase(options) { IsTest = true };

            DbContext.Seed();

            AppSettings = AppSettings.Default;

            AccountService = new AccountService(new AccountRepository(DbContext), AppSettings);

            AccountService.IsValid(CryptoHelper.Guid("U$1"), out AccountTicket accountTicket);

            AccountTicket = accountTicket;
        }

        [TearDown]
        public virtual void Down()
        {
            DbContext.Database
                .EnsureDeleted();
        }
    }
}