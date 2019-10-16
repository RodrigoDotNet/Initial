using Initial.Api.Models.Database;
using Initial.Api.Models.Interfaces;

namespace Initial.Api.Models
{
    public partial interface IGroupRepository
        : IPrivateRepository<Group>
    {
    }
}
