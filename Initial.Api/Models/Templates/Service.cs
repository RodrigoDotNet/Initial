using Initial.Api.Util;

namespace Initial.Api.Models.Templates
{
    public abstract class Service<TRepository>
    {
        protected readonly TRepository _repository;

        protected readonly AppSettings _appSettings;

        protected readonly StateResponse State
            = new StateResponse();

        public Service(TRepository repository, AppSettings appSettings)
        {
            _repository = repository;
            _appSettings = appSettings;
        }
    }
}
