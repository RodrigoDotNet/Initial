using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models
{
    public partial class CustomerRepository
        : PrivateRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InitialDatabase database)
            : base(database)
        {
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync(AccountTicket user)
        {
            return await _database.Customers
                .AsNoTracking()
                .Where(e => e.EnterpriseId == user.EnterpriseId)
                .ToListAsync();
        }

        public override async Task<Customer> GetAsync(AccountTicket user, int id)
        {
            return await _database.Customers
                .Where(e => e.EnterpriseId == user.EnterpriseId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task SaveAsync(AccountTicket user, Customer model)
        {
            if (model.Id > 0)
            {
                _database.Entry(model).State = EntityState.Modified;
            }
            else
            {
                await _database.Customers.AddAsync(model);
            }

            await _database.SaveChangesAsync();

            return;
        }
    }
}
