using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IPrivateService<RQ, RS>
    {
        Task<IActionResult> GetAllAsync(AccountTicket user);

        Task<IActionResult> GetAsync(AccountTicket user, int id);

        Task<IActionResult> RemoveAsync(AccountTicket user, int id);

        Task<IActionResult> DeleteAsync(AccountTicket user, int id);

        Task<IActionResult> PostAsync(AccountTicket user, RQ request);

        Task<IActionResult> PutAsync(AccountTicket user, int id, RQ request);
    }
}
