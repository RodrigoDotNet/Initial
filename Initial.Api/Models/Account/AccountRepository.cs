using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public partial class AccountRepository
        : PrivateRepository<User>, IAccountRepository
    {
        public AccountRepository(InitialDatabase database)
            : base(database)
        {
        }

        public async Task<User> GetAsync(int id)
        {
            return await _database.Users
                .FindAsync(id);
        }

        public virtual async Task<User> GetByEmailPasswordAsync
            (string email, Guid password)
        {
            return await _database.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e =>
                    e.Email == email
                    && e.Password == password
                );
        }

        public async Task<User> GetByPublicIdAsync
            (Guid publicId)
        {
            return await _database.Users
                .FirstOrDefaultAsync(e =>
                    e.PublicId == publicId
                );
        }

        public async Task<IEnumerable<AreaAccess>> GetAreaAccess
            (AccountTicket user, int areaId)
        {
            return await _database.AreaAccess
                .AsNoTracking()
                .Include(e => e.Group.UserGroups)

                .Where(e => e.Group.UserGroups
                    .Any(ug => ug.UserId == user.Id)
                )
                .Where(e => e.AreaId == areaId)

                .ToListAsync();
        }

        public async Task<bool> HasPolicyAccess
            (AccountTicket user, int policyId)
        {
            return await _database.PolicyAccess
                .AsNoTracking()
                .Include(e => e.Group.UserGroups)

                .Where(e => e.Group.UserGroups
                    .Any(ug => ug.UserId == user.Id)
                )
                .Where(e => e.PolicyId == policyId)

                .AnyAsync();
        }
    }
}