namespace Initial.Api.Tests.Controllers
{
    public abstract class ControllerServiceBaseTest
        <TRepository, TService, TController>
        : ControllerDefaultBaseTest<TController>
    {
        protected TRepository Repository { get; set; }

        protected TService Service { get; set; }
    }
}
