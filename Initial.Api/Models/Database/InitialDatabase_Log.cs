using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase
    {
        public virtual DbSet<Log> Logs { get; set; }

        private void OnModelCreating_Log(ModelBuilder modelBuilder) { }

        private void Seed_Log() { }
    }
}
