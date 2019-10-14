using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public partial class AccountRepository
        : Repository<User>, IAccountRepository
    {
        public AccountRepository(InitialDatabase database)
            : base(database)
        {
        }

        public override async Task<User> GetAsync(int id)
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

        public async Task<User> GetByPublicIdAsync(Guid publicId)
        {
            return await _database.Users
                .FirstOrDefaultAsync(e =>
                    e.PublicId == publicId
                );
        }

        public override async Task SaveAsync(User model)
        {
            if (model.Id > 0)
            {
                _database.Entry(model).State = EntityState.Modified;
            }
            else
            {
                await _database.Users.AddAsync(model);
            }

            await _database.SaveChangesAsync();

            return;
        }
    }
}