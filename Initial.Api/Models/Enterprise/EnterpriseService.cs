using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Initial.Api.Util;

namespace Initial.Api.Models
{
    public partial class EnterpriseService
        : PrivateService<Enterprise, EnterpriseRequest, EnterpriseResponse>, IEnterpriseService
    {
        public EnterpriseService(IEnterpriseRepository repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        protected override EnterpriseResponse Parse(Enterprise model)
        {
            return new EnterpriseResponse
            {
                Id = model.Id,
                Name = model.Name,
                EntityVersion = model.LastModifiedDate
            };
        }
    }
}
