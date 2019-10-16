using Initial.Api.Models.Interfaces;

namespace Initial.Api.Models
{
    public partial interface IGroupService
        : IPrivateService<GroupRequest, GroupResponse>
    {

    }
}
