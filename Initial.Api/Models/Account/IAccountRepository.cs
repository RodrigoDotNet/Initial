using Initial.Api.Models.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public partial interface IAccountRepository
    {
        Task<User> GetByEmailPasswordAsync
            (string email, Guid password);

        Task<User> GetByPublicIdAsync
            (Guid publicId);

        Task<IEnumerable<AreaAccess>> GetAreaAccess
            (AccountTicket user, int areaId);

        Task<bool> HasPolicyAccess
             (AccountTicket user, int policyId);
    }
}