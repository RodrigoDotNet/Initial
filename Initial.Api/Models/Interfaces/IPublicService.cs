using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Initial.Api.Models.Interfaces
{
    public interface IPublicService
    {
        Task<IActionResult> GetAllAsync();

        Task<IActionResult> GetAsync(int id);
    }
}
