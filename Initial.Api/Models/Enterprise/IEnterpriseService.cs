using Initial.Api.Models.Interfaces;

namespace Initial.Api.Models
{
    public partial interface IEnterpriseService
        : IPrivateService<EnterpriseRequest, EnterpriseResponse>
    { }
}
