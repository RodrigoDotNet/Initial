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

            if (context.Enterprises.Any())
            {
                return;
            }

            var enterprises = new Enterprise[]
            {
                new Enterprise{
                    Name ="LM",
                    PrivateId = CryptoHelper.Guid("E$1"),
                    PublicId = CryptoHelper.Guid("EP1")
                }
            };

            context.Enterprises.AddRange(enterprises);

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{
                    Name ="LM",
                    PrivateId = CryptoHelper.Guid("U$1"),
                    PublicId = CryptoHelper.Guid("UP1"),
                    Email = "lm@lm.com.br",
                    Password = CryptoHelper.Guid("lm2019"),
                    EnterpriseId = enterprises[0].Id
                }
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
