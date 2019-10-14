using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IPrivateRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(AccountTicket user);

        Task<T> GetAsync(AccountTicket user, int id);

        Task<T> RemoveAsync(AccountTicket user, int id);

        Task<T> DeleteAsync(AccountTicket user, int id);

        Task SaveAsync(AccountTicket user, T model);
    }
}
