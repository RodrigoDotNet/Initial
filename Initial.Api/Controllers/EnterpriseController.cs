using System.Threading.Tasks;
using Initial.Api.Controllers.Templates;
using Initial.Api.Filters;
using Initial.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Initial.Api.Controllers
{
    /// <summary>
    /// Métodos de empresa (Privada)
    /// </summary>
    [Authorize]
    [AuthorizeFilter]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class EnterpriseController
        : ControllerServiceBase<IEnterpriseService>
    {
        public EnterpriseController
            (IEnterpriseService service)
            : base(service) { }

        // GET api/Enterprise

        /// <summary>
        /// Recuperar todos itens
        /// </summary>
        /// <returns>itens desejados</returns>
        [HttpGet]
        [ProducesResponseType(typeof(EnterpriseResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/Enterprise/5

        /// <summary>
        /// Recuperar um item
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item desejado</returns>
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

        #endregion
    }
}
