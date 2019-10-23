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
    /// Métodos de grupos (Pública e Privada)
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class GroupController
        : ControllerServiceBase<IGroupService>
    {
        public GroupController
            (IGroupService service)
            : base(service) { }

        // GET api/Group

        /// <summary>
        /// Recuperar todos itens (Pública + Privada, se autenticado)
        /// </summary>
        /// <returns>itens desejados</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/Group/5

        /// <summary>
        /// Recuperar um item (Pública + Privada, se autenticado)
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item desejado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(AccountTicket, id);
        }

        // DELETE api/Group/5

        /// <summary>
        /// Excluir um item (Privada)
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item excluído</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
        [Authorize]
        [AuthorizeFilter(AreaEnum.Group, ModeEnum.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return await RepositoryService.DeleteAsync(AccountTicket, id);
        }

        // POST api/Group

        /// <summary>
        /// Criar um item (Privada)
        /// </summary>
        /// <param name="request">Definição do item</param>
        /// <returns>Item criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
        [Authorize]
        [AuthorizeFilter(AreaEnum.Group, ModeEnum.Create)]
        public async Task<IActionResult> Post
            ([FromBody]GroupRequest request)
        {
            return await RepositoryService.PostAsync(AccountTicket, request);
        }

        // PUT api/Group/4

        /// <summary>
        /// Alterar um item (Privada)
        /// </summary>
        /// <param name="request">Definição do item</param>
        /// <returns>Item alterado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
        [Authorize]
        [AuthorizeFilter(AreaEnum.Group, ModeEnum.Modify)]
        public async Task<IActionResult> Put
            (int id, [FromBody]GroupRequest request)
        {
            return await RepositoryService.PutAsync(AccountTicket, id, request);
        }

        #region Examples

        private class GetAllResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new[]
                {
                    new GroupResponse
                    {
                        Id = 1,
                        Name = "Group A"
                    },
                    new GroupResponse
                    {
                        Id = 2,
                        Name = "Group B"
                    }
                };
            }
        }

        private class GetResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new GroupResponse
                {
                    Id = 1,
                    Name = "Group A"
                };
            }
        }

        private class PostRequest : IExamplesProvider
        {
            public object GetExamples()
            {
                return new GroupRequest
                {
                    Name = "Group C"
                };
            }
        }

        #endregion
    }
}
