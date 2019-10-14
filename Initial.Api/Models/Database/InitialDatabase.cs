using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase : DbContext
    {
        public InitialDatabase(DbContextOptions<InitialDatabase> options)
            : base(options)
        {

        }

        public virtual DbSet<Log> Logs { get; set; }

        public virtual DbSet<Enterprise> Enterprises { get; set; }

        #region Security

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<UserGroup> UserGroups { get; set; }

        public virtual DbSet<Area> Areas { get; set; }

        public virtual DbSet<AreaAccess> AreaAccess { get; set; }

        public virtual DbSet<Policy> Policies { get; set; }

        public virtual DbSet<PolicyAccess> PolicyAccess { get; set; }

        #endregion

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Enterprise");

            #region Security

            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("User");

            modelBuilder.Entity<Group>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Group");

            modelBuilder.Entity<UserGroup>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("UserGroup");

            modelBuilder.Entity<Area>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Area");

            modelBuilder.Entity<AreaAccess>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("AreaAccess");

            modelBuilder.Entity<Policy>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Policy");

            modelBuilder.Entity<PolicyAccess>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("PolicyAccess");

            #endregion

            modelBuilder.Entity<Customer>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Customer");
        }

    }
}
