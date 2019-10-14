using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<T> RemoveAsync(int id);

        Task SaveAsync(T model);
    }
}
