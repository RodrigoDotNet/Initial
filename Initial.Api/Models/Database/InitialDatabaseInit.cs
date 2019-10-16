using Initial.Api.Resources;
using Initial.Api.Util;
using System;
using System.Linq;

namespace Initial.Api.Models.Database
{
    public static partial class InitialDatabaseInit
    {
        public static bool EnsureCreated { get; set; } = true;

        public static void Initialize(InitialDatabase context)
        {
            if (EnsureCreated)
                context.Database.EnsureCreated();

            if (!context.Enterprises.Any())
            {
                {
                    var enterprise = new Enterprise
                    {
                        Name = $"Enterprise A",
                        PrivateId = CryptoHelper.Guid($"EA#"),
                        PublicId = CryptoHelper.Guid($"EA$")
                    };

                    context.Enterprises.Add(enterprise);
                }

                for (var i = 1; i <= 5; i++)
                {
                    var enterprise = new Enterprise
                    {
                        Name = $"Enterprise B{i}",
                        PrivateId = CryptoHelper.Guid($"E#{i}"),
                        PublicId = CryptoHelper.Guid($"E${i}")
                    };

                    context.Enterprises.Add(enterprise);
                }

                context.SaveChanges();
            }

            #region Security

            if (!context.Groups.Any())
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

                context.Groups.AddRange(groups);

                foreach (var enterprise in context.Enterprises)
                {
                    var user = new Group
                    {
                        Name = $"{enterprise.Name}'s User",
                        Enterprise = enterprise
                    };

                    context.Groups.Add(user);
                }

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User{
                        Name ="User",
                        PrivateId = CryptoHelper.Guid("U$1"),
                        PublicId = CryptoHelper.Guid("UP1"),
                        Email = "user@enterprisea.com",
                        Password = CryptoHelper.Guid("user2019"),
                        Enterprise = context.Enterprises
                            .First(e => e.Name == "Enterprise A")
                    }
                };

                context.Users.AddRange(users);

                foreach (var group in context.Groups
                    .Where(e => e.EnterpriseId == null))
                {
                    foreach (var enterprise in context.Enterprises)
                    {
                        var user = new User
                        {
                            Name = group.Name,
                            PrivateId = CryptoHelper.Guid(group.Name + "$" + enterprise.Name),
                            PublicId = CryptoHelper.Guid(group.Name + "P" + enterprise.Name),
                            Email = $"{group.Name}@{enterprise.Name}.com".Replace(" ", "").ToLower(),
                            Password = CryptoHelper.Guid(group.Name.ToLower()),
                            Enterprise = enterprise
                        };

                        context.Users.Add(user);

                        var userGroup = new UserGroup
                        {
                            User = user,
                            Group = group
                        };

                        context.UserGroups.AddRange(userGroup);
                    }
                }

                context.SaveChanges();
            }

            if (!context.Areas.Any())
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

                context.Areas.AddRange(areas);

                context.SaveChanges();
            }

            if (!context.AreaAccess.Any())
            {
                {
                    var area = context.Areas
                      .Find((int)AreaEnum.Customer);

                    var group = context.Groups
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

                    context.AreaAccess.Add(areaAccess);
                }

                {
                    var area = context.Areas
                      .Find((int)AreaEnum.Customer);

                    var group = context.Groups
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

                    context.AreaAccess.Add(areaAccess);
                }

                {
                    var area = context.Areas
                      .Find((int)AreaEnum.Customer);

                    var group = context.Groups
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

                    context.AreaAccess.Add(areaAccess);
                }

                {
                    var area = context.Areas
                      .Find((int)AreaEnum.Group);

                    var group = context.Groups
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

                    context.AreaAccess.Add(areaAccess);
                }

                {
                    var area = context.Areas
                      .Find((int)AreaEnum.User);

                    var group = context.Groups
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

                    context.AreaAccess.Add(areaAccess);
                }

                context.SaveChanges();
            }

            if (!context.Policies.Any())
            {
                var area = context.Areas
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

                context.Policies.AddRange(policies);

                context.SaveChanges();
            }

            if (!context.PolicyAccess.Any())
            {
                var area = context.Areas
                    .Find((int)AreaEnum.User);

                var group = context.Groups
                    .First(e => e.Name == "Security");

                foreach (var policy in context.Policies
                    .Where(e => e.AreaId == area.Id))
                {
                    var policyAccess = new PolicyAccess
                    {
                        Group = group,
                        Policy = policy
                    };

                    context.PolicyAccess.AddRange(policyAccess);
                }

                context.SaveChanges();
            }

            #endregion

            #region CRM

            if (!context.Customers.Any())
            {
                foreach (var enterprise in context.Enterprises)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var guid = Guid.NewGuid().ToString().Substring(0, 4);

                        var customer = new Customer
                        {
                            Enterprise = enterprise,
                            Email = Examples.Email,
                            Name = $"Customers {guid}"
                        };

                        context.Customers.Add(customer);
                    }
                }

                context.SaveChanges();
            }

            #endregion
        }
    }
}
