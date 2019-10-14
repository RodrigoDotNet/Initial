using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Initial.Api.Resources;
using Initial.Api.Util;
using System;

namespace Initial.Api.Models
{
    public partial class CustomerService
        : PrivateService<Customer, CustomerRequest, CustomerResponse>, ICustomerService
    {
        public CustomerService(ICustomerRepository repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        protected override CustomerResponse Parse(Customer model)
        {
            return new CustomerResponse
            {
                Id = model.Id,
                Name = model.Name,
                EntityVersion = model.LastModifiedDate
            };
        }

        protected override Customer Parse(AccountTicket user, CustomerRequest request)
        {
            return new Customer
            {
                Name = request.Name,
                EnterpriseId = user.EnterpriseId,

                CreationDate = DateTime.Now,
                CreationUserId = user.Id,
                LastModifiedDate = DateTime.Now,
                LastModifiedUserId = user.Id
            };
        }

        protected override void Merge(AccountTicket user, Customer model, CustomerRequest request)
        {
            model.Name = request.Name;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }

        protected override void AuditDelete(AccountTicket user, Customer model)
        {
            model.Inactive = true;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }

        protected override bool Validate
            (AccountTicket user, CustomerRequest request)
        {
            return base.Validate(user, request);
        }

        protected override bool Validate
            (AccountTicket user, CustomerRequest request, Customer model)
        {
            if (model.Id > 0)
            {
                if (model.EnterpriseId != user.EnterpriseId)
                {
                    State.Add("EntityVersion", Messages.EnterpriseId_Invalid);

                    return false;
                }

                if (request.EntityVersion != null
                    && request.EntityVersion != model.LastModifiedDate)
                {
                    State.Add("EntityVersion", Messages.EntityVersion_Conflict);

                    return false;
                }
            }

            return base.Validate(user, request, model);
        }
    }
}
