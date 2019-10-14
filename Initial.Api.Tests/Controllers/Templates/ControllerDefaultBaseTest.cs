using Initial.Api.Models;
using Initial.Api.Models.Database;
using Initial.Api.Util;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Tests.Controllers
{
    public abstract class ControllerDefaultBaseTest
        <TController>
    {
        protected InitialDatabase Database { get; private set; }

        public AppSettings AppSettings { get; private set; }

        public AccountService AccountService { get; private set; }

        protected TController Controller { get; set; }

        protected AccountTicket AccountTicket { get; set; }

        public virtual void Setup()
        {
            var options = new DbContextOptionsBuilder<InitialDatabase>()
                .UseInMemoryDatabase(databaseName: "Initial_Api")
                .Options;

            Database = new InitialDatabase(options);

            InitialDatabaseInit.EnsureCreated = false;

            InitialDatabaseInit.Initialize(Database);

            AppSettings = AppSettings.Default;

            AccountService = new AccountService(new AccountRepository(Database), AppSettings);

            AccountService.IsValid(CryptoHelper.Guid("UP1"), out AccountTicket accountTicket);

            AccountTicket = accountTicket;
        }
    }
}