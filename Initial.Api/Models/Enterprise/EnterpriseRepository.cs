using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models
{
    public partial class EnterpriseRepository
        : PrivateRepository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(InitialDatabase database)
            : base(database) { }

        public override async Task<IEnumerable<Enterprise>> GetAllAsync(AccountTicket user)
        {
            return await _database.Enterprises
                .AsNoTracking()
                .Where(e => e.Id == user.EnterpriseId)
                .ToListAsync();
        }

        public override async Task<Enterprise> GetAsync(AccountTicket user, int id)
        {
            return await _database.Enterprises
                .Where(e => e.Id == user.EnterpriseId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
