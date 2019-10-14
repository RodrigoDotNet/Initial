using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase : DbContext
    {
        public InitialDatabase(DbContextOptions<InitialDatabase> options)
            : base(options)
        {

        }

        public virtual DbSet<Enterprise> Enterprises { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .HasQueryFilter(e => !e.Deleted)
                .ToTable("Enterprise");

            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.Deleted)
                .ToTable("User");
        }

    }
}
