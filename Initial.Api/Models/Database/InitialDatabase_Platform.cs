using System.Linq;
using Initial.Api.Util;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase
    {
        public virtual DbSet<Enterprise> Enterprises { get; set; }

        private void OnModelCreating_Platform(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Enterprise");
        }

        /// <summary>
        /// Criando empresas de teste
        /// </summary>
        private void Seed_Platform()
        {
            var letters = new[] { 'A', 'B', 'C', 'D', 'E' };

            if (IsTest)
            {
                if (!Enterprises.Any())
                {
                    {
                        var enterprise = new Enterprise
                        {
                            Name = $"Enterprise Test",
                            PrivateId = CryptoHelper.Guid($"ET#"),
                            PublicId = CryptoHelper.Guid($"ET$")
                        };

                        Enterprises.Add(enterprise);
                    }

                    foreach (var letter in letters)
                    {
                        var enterprise = new Enterprise
                        {
                            Name = $"Enterprise {letter}",
                            PrivateId = CryptoHelper.Guid($"E{letter}#"),
                            PublicId = CryptoHelper.Guid($"E{letter}$")
                        };

                        Enterprises.Add(enterprise);
                    }

                    SaveChanges();
                }
            }
        }
    }
}
