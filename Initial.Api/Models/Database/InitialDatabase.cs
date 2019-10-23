using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase : DbContext
    {
        public bool IsInMemory => Database.IsInMemory();

#if DEBUG
        public bool IsTest { get; set; } = true;
#else
        public bool IsTest { get; set; } = false;
#endif

        public InitialDatabase(DbContextOptions<InitialDatabase> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating_Log(modelBuilder);

            OnModelCreating_Platform(modelBuilder);

            OnModelCreating_Security(modelBuilder);

            OnModelCreating_Crm(modelBuilder);
        }

        public void Seed()
        {
            if (!IsInMemory) Database.EnsureCreated();

            Seed_Log();

            Seed_Platform();

            Seed_Security();

            Seed_Crm();
        }
    }
}
