using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using System;

namespace Initial.Api.Models
{
    public partial class EnterpriseService
        : Service<Enterprise, EnterpriseRequest, EnterpriseResponse>, IEnterpriseService
    {
        public EnterpriseService(IEnterpriseRepository repository)
            : base(repository)
        {

        }

        protected override EnterpriseResponse Parse(Enterprise model)
        {
            return new EnterpriseResponse
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override Enterprise Parse(AccountTicket user, EnterpriseRequest request)
        {
            return new Enterprise
            {
                Name = request.Name,
                CreationDate = DateTime.Now,
                CreationUserId = user.Id,
            };
        }

        protected override void Merge(AccountTicket user, Enterprise model, EnterpriseRequest request)
        {
            model.Name = request.Name;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }

        protected override void Delete(AccountTicket user, Enterprise model)
        {
            model.Deleted = true;
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedUserId = user.Id;
        }
    }
}
