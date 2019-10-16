using System.Threading.Tasks;
using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Initial.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class AreaController
        : ControllerServiceBase<IAreaService>
    {
        public AreaController
            (IAreaService service)
            : base(service) { }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(id);
        }

        #region Examples

        private class GetAllResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new[]
                {
                    new AreaResponse
                    {
                        Id = 1,
                        Name = "Area A"
                    },
                    new AreaResponse
                    {
                        Id = 2,
                        Name = "Area B"
                    }
                };
            }
        }

        private class GetResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new AreaResponse
                {
                    Id = 1,
                    Name = "Area A"
                };
            }
        }

        #endregion
    }
}
