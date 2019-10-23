using Initial.Api.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<UserGroup> UserGroups { get; set; }

        public virtual DbSet<Area> Areas { get; set; }

        public virtual DbSet<AreaAccess> AreaAccess { get; set; }

        public virtual DbSet<Policy> Policies { get; set; }

        public virtual DbSet<PolicyAccess> PolicyAccess { get; set; }

        private void OnModelCreating_Security(ModelBuilder modelBuilder)
        {
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
        }

        /// <summary>
        /// Criando regras iniciais e de testes
        /// </summary>
        private void Seed_Security()
        {
            if (!Groups.Any())
            {
                var groups = new Group[]
                {
                    new Group{
                        Name ="Security"
                    },
                    new Group{
                        Name ="Management"
                    },
                    new Group{
                        Name ="Configuration"
                    },
                    new Group{
                        Name ="Sales"
                    },
                    new Group{
                        Name ="Operation"
                    }
                };

                Groups.AddRange(groups);

                foreach (var enterprise in Enterprises)
                {
                    var name = enterprise.Name.EndsWith('s')
                        ? enterprise.Name + "\'s"
                        : enterprise.Name;

                    var group = new Group
                    {
                        Name = $"{name} User",
                        Enterprise = enterprise
                    };

                    Groups.Add(group);
                }

                SaveChanges();
            }

            if (IsTest)
            {
                if (!Users.Any())
                {
                    {
                        var enterprise = Enterprises
                             .Single(e => e.Name == "Enterprise Test");

                        var email = $"user@{enterprise.Name}.com".Replace(" ", "").ToLower();

                        var user = new User
                        {
                            Name = "User",
                            PublicId = CryptoHelper.Guid("U$1"),
                            PrivateId = CryptoHelper.Guid("U$1"),
                            Email = email,
                            Password = CryptoHelper.Guid("user2019"),
                            Enterprise = enterprise
                        };

                        Users.Add(user);
                    }

                    foreach (var group in Groups
                        .Where(e => e.EnterpriseId == null))
                    {
                        foreach (var enterprise in Enterprises)
                        {
                            var email = $"{group.Name}@{enterprise.Name}.com".Replace(" ", "").ToLower();

                            var user = new User
                            {
                                Name = group.Name,
                                PublicId = CryptoHelper.Guid(group.Name + "#" + enterprise.Name),
                                PrivateId = CryptoHelper.Guid(group.Name + "$" + enterprise.Name),
                                Email = email,
                                Password = CryptoHelper.Guid(group.Name.ToLower()),
                                Enterprise = enterprise
                            };

                            Users.Add(user);

                            var userGroup = new UserGroup
                            {
                                User = user,
                                Group = group
                            };

                            UserGroups.AddRange(userGroup);
                        }
                    }

                    SaveChanges();
                }
            }

            if (!Areas.Any())
            {
                var areas = new Area[]
                {
                    new Area{
                        Id = (int)AreaEnum.User,
                        Name = "User"
                    },
                    new Area{
                        Id = (int)AreaEnum.Group,
                        Name = "Group"
                    },
                    new Area{
                        Id = (int)AreaEnum.Customer,
                        Name = "Customer"
                    }
                };

                Areas.AddRange(areas);

                SaveChanges();
            }

            if (IsTest)
            {
                if (!AreaAccess.Any())
                {
                    // Sales tem acesso de leitura e escrita no CRM

                    {
                        var area = Areas
                          .Find((int)AreaEnum.Customer);

                        var group = Groups
                            .First(e => e.Name == "Sales");

                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = true,
                            CanDelete = false,
                            CanModify = true,
                            CanRead = true
                        };

                        AreaAccess.Add(areaAccess);
                    }

                    // Management tem acesso completo ao CRM

                    {
                        var area = Areas
                          .Find((int)AreaEnum.Customer);

                        var group = Groups
                            .First(e => e.Name == "Management");

                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = true,
                            CanDelete = true,
                            CanModify = true,
                            CanRead = true
                        };

                        AreaAccess.Add(areaAccess);
                    }

                    // Operation só pode consultar o CRM

                    {
                        var area = Areas
                          .Find((int)AreaEnum.Customer);

                        var group = Groups
                            .First(e => e.Name == "Operation");

                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = false,
                            CanDelete = false,
                            CanModify = false,
                            CanRead = true
                        };

                        AreaAccess.Add(areaAccess);
                    }

                    // Segurança pode criar novos grupos

                    {
                        var area = Areas
                          .Find((int)AreaEnum.Group);

                        var group = Groups
                            .First(e => e.Name == "Security");

                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = true,
                            CanDelete = true,
                            CanModify = true,
                            CanRead = true
                        };

                        AreaAccess.Add(areaAccess);
                    }

                    // Segurança pode criar novos usuários

                    {
                        var area = Areas
                          .Find((int)AreaEnum.User);

                        var group = Groups
                            .First(e => e.Name == "Security");

                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = true,
                            CanDelete = true,
                            CanModify = true,
                            CanRead = true
                        };

                        AreaAccess.Add(areaAccess);
                    }

                    SaveChanges();
                }
            }

            if (!Policies.Any())
            {
                var area = Areas
                    .Find((int)AreaEnum.User);

                var policies = new Policy[]
                {
                    new Policy{
                        Id = (int)PolicyEnum.User_ChangePassword,
                        Area = area,
                        Name = "Change Password"
                    },
                    new Policy{
                        Id = (int)PolicyEnum.User_ChangeEmail,
                        Area = area,
                        Name = "Change Email"
                    }
                };

                Policies.AddRange(policies);

                SaveChanges();
            }

            if (IsTest)
            {
                if (!PolicyAccess.Any())
                {
                    // Segurança tem acesso às políticas de segurança

                    {
                        var area = Areas
                            .Find((int)AreaEnum.User);

                        var group = Groups
                            .First(e => e.Name == "Security");

                        foreach (var policy in Policies
                            .Where(e => e.AreaId == area.Id))
                        {
                            var policyAccess = new PolicyAccess
                            {
                                Group = group,
                                Policy = policy
                            };

                            PolicyAccess.AddRange(policyAccess);
                        }
                    }

                    SaveChanges();
                }
            }
        }
    }
}
