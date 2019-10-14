using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IService<RQ, RS>
    {
        Task<IActionResult> GetAllAsync();

        Task<IActionResult> GetAsync(int id);

        Task<IActionResult> RemoveAsync(int id);

        Task<IActionResult> DeleteAsync(AccountTicket user, int id);

        Task<IActionResult> PostAsync(AccountTicket user, RQ request);

        Task<IActionResult> PutAsync(AccountTicket user, int id, RQ request);
    }
}
