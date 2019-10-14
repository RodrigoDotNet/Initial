using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IPublicRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);
    }
}
