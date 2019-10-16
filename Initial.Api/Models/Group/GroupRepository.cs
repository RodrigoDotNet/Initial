using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models
{
    public partial class GroupRepository
        : PrivateRepository<Group>, IGroupRepository
    {
        public GroupRepository(InitialDatabase database)
            : base(database)
        {
        }

        public override async Task<IEnumerable<Group>> GetAllAsync(AccountTicket user)
        {
            var enterpriseId = user?.EnterpriseId;

            return await _database.Groups
                .AsNoTracking()
                .Where(e => e.EnterpriseId == null || e.EnterpriseId == enterpriseId)
                .ToListAsync();
        }

        public override async Task<Group> GetAsync(AccountTicket user, int id)
        {
            var enterpriseId = user?.EnterpriseId;

            return await _database.Groups
                .Where(e => e.EnterpriseId == null || e.EnterpriseId == enterpriseId)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task SaveAsync(AccountTicket user, Group model)
        {
            if (model.Id > 0)
            {
                _database.Entry(model).State = EntityState.Modified;
            }
            else
            {
                await _database.Groups.AddAsync(model);
            }

            await _database.SaveChangesAsync();

            return;
        }
    }
}
