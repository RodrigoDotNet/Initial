using Initial.Api.Models.Interfaces;

namespace Initial.Api.Models
{
    public partial interface ICustomerService
        : IPrivateService<CustomerRequest, CustomerResponse>
    {

    }
}
