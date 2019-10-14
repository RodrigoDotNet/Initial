namespace Initial.Api.Controllers.Templates
{
    public abstract class ControllerServiceBase<TService>
        : ControllerDefaultBase
    {
        protected readonly TService RepositoryService;

        public ControllerServiceBase
            (TService service)
            : base()
        {
            RepositoryService = service;
        }
    }
}