using Initial.Api.Resources;
using Initial.Api.Util;
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
                        Name = $"LM",
                        PrivateId = CryptoHelper.Guid($"LM#"),
                        PublicId = CryptoHelper.Guid($"LM$")
                    };

                    context.Enterprises.Add(enterprise);
                }

                for (int i = 0; i < 5; i++)
                {
                    var enterprise = new Enterprise
                    {
                        Name = $"Ent {i}",
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
                        Name ="Configuração"
                    },
                    new Group{
                        Name ="Operação"
                    }
                };

                context.Groups.AddRange(groups);

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User{
                        Name ="LM",
                        PrivateId = CryptoHelper.Guid("U$1"),
                        PublicId = CryptoHelper.Guid("UP1"),
                        Email = "lm@lm.com.br",
                        Password = CryptoHelper.Guid("lm2019"),
                        Enterprise = context.Enterprises.First(e => e.Name == "LM")
                    }
                };

                context.Users.AddRange(users);

                context.SaveChanges();
            }

            if (!context.UserGroups.Any())
            {
                foreach (var user in context.Users)
                {
                    foreach (var group in context.Groups)
                    {
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
                        Name ="Enterprise"
                    },
                    new Area{
                        Name ="Customer"
                    }
                };

                context.Areas.AddRange(areas);

                context.SaveChanges();
            }

            if (!context.AreaAccess.Any())
            {
                foreach (var area in context.Areas)
                {
                    foreach (var group in context.Groups)
                    {
                        var areaAccess = new AreaAccess
                        {
                            Area = area,
                            Group = group,
                            CanCreate = true,
                            CanDelete = true,
                            CanModify = true,
                            CanRead = true
                        };

                        context.AreaAccess.AddRange(areaAccess);
                    }
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
                        var customer = new Customer
                        {
                            Enterprise = enterprise,
                            Email = Examples.Email,
                            Name = $"{enterprise.Name} - Cus {i}"
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
