using System.Collections.Generic;
using System.Threading.Tasks;
using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models
{
    public partial class EnterpriseRepository
        : Repository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(InitialDatabase database)
            : base(database)
        {
        }

        public override async Task<IEnumerable<Enterprise>> GetAllAsync()
        {
            return await _database.Enterprises
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Enterprise> GetAsync(int id)
        {
            return await _database.Enterprises
                .FindAsync(id);
        }

        public override async Task<Enterprise> RemoveAsync(int id)
        {
            var model = await _database.Enterprises
                .FindAsync(id);

            if (model == null) return null;

            _database.Enterprises.Remove(model);

            await _database.SaveChangesAsync();

            return model;
        }

        public override async Task SaveAsync(Enterprise model)
        {
            if (model.Id > 0)
            {
                _database.Entry(model).State = EntityState.Modified;
            }
            else
            {
                await _database.Enterprises.AddAsync(model);
            }

            await _database.SaveChangesAsync();

            return;
        }
    }
}
