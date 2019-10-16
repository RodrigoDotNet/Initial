using Initial.Api.Models.Database;
using Initial.Api.Models.Templates;
using Initial.Api.Util;

namespace Initial.Api.Models
{
    public partial class AreaService
        : PublicService<Area, AreaResponse>, IAreaService
    {
        public AreaService(IAreaRepository repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        protected override AreaResponse Parse(Area model)
        {
            return new AreaResponse
            {
                Id = model.Id,
                Name = model.Name,
                EntityVersion = model.LastModifiedDate
            };
        }
    }
}
