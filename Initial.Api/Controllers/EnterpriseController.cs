using System.Threading.Tasks;
using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Initial.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class EnterpriseController
        : ControllerServiceBase<IEnterpriseService>
    {
        public EnterpriseController
            (IEnterpriseService service)
            : base(service) { }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(EnterpriseResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnterpriseResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(AccountTicket, id);
        }

        #region Examples

        private class GetAllResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new[]
                {
                    new EnterpriseResponse
                    {
                        Id = 1,
                        Name = "Enterprise A"
                    },
                    new EnterpriseResponse
                    {
                        Id = 2,
                        Name = "Enterprise B"
                    }
                };
            }
        }

        private class GetResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new EnterpriseResponse
                {
                    Id = 1,
                    Name = "Enterprise A"
                };
            }
        }

        private class PostRequest : IExamplesProvider
        {
            public object GetExamples()
            {
                return new EnterpriseRequest
                {
                    Name = "Enterprise C"
                };
            }
        }

        #endregion
    }
}
