using System.Linq;
using Initial.Api.Resources;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models.Database
{
    public partial class InitialDatabase
    {
        public virtual DbSet<Customer> Customers { get; set; }

        private void OnModelCreating_Crm(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasQueryFilter(e => !e.Inactive)
                .ToTable("Customer");
        }

        /// <summary>
        /// Criando um cliente para cada empresa
        /// </summary>
        private void Seed_Crm()
        {
            var letters = new[] { 'A', 'B', 'C', 'D', 'E' };

            if (IsTest)
            {
                if (!Customers.Any())
                {
                    {
                        var enterprise = Enterprises
                            .Single(e => e.Name == "Enterprise Test");

                        var customer = new Customer
                        {
                            Enterprise = enterprise,
                            Email = Examples.Email,
                            Name = $"Customer Test"
                        };

                        Customers.Add(customer);
                    }

                    foreach (var enterprise in Enterprises)
                    {
                        foreach (var letter in letters)
                        {
                            var customer = new Customer
                            {
                                Enterprise = enterprise,
                                Email = Examples.Email,
                                Name = $"Customer {letter}"
                            };

                            Customers.Add(customer);
                        }
                    }

                    SaveChanges();
                }
            }
        }
    }
}
