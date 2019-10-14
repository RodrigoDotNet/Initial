using Initial.Api.Models.Database;
using Initial.Api.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public partial interface IAccountRepository
        : IRepository<User>
    {
        Task<User> GetByEmailPasswordAsync
            (string email, Guid password);

        Task<User> GetByPublicIdAsync
            (Guid publicId);
    }
}