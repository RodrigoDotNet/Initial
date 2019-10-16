using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public interface IAccountService
    {
        Task<IActionResult> Login(AccountLoginRequest request);

        bool IsValid(Guid publicId, out AccountTicket ticket);

        Task<ModeEnum> GetAccessAreaMode(AccountTicket ticket, AreaEnum area);

        Task<bool> HasPolicyAccess(AccountTicket ticket, PolicyEnum policy);
    }
}
