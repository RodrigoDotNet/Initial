using System.Threading.Tasks;
using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Initial.Api.Controllers
{
    /// <summary>
    /// Métodos de áreas do sistema (Privada)
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class AreaController
        : ControllerServiceBase<IAreaService>
    {
        public AreaController
            (IAreaService service)
            : base(service) { }

        // GET api/Area

        /// <summary>
        /// Recuperar todos itens
        /// </summary>
        /// <returns>itens desejados</returns>
        [HttpGet]
        [ProducesResponseType(typeof(AreaResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync();
        }

        // GET api/Area/5

        /// <summary>
        /// Recuperar um item
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item desejado</returns>
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
