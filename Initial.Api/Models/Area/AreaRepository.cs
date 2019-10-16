using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Microsoft.EntityFrameworkCore;

namespace Initial.Api.Models
{
    public partial class AreaRepository
        : PublicRepository<Area>, IAreaRepository
    {
        public AreaRepository(InitialDatabase database)
            : base(database) { }

        public override async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _database.Areas
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Area> GetAsync(int id)
        {
            return await _database.Areas
                .FindAsync(id);
        }
    }
}
