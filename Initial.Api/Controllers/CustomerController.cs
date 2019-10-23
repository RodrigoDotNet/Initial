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
    /// Métodos de clientes (Privada)
    /// </summary>
    [Authorize]
    [AuthorizeFilter]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class CustomerController 
        : ControllerServiceBase<ICustomerService>
    {
        public CustomerController
            (ICustomerService service)
            : base(service) { }

        // GET api/Customer

        /// <summary>
        /// Recuperar todos itens
        /// </summary>
        /// <returns>itens desejados</returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        [AuthorizeFilter(AreaEnum.Customer, ModeEnum.Read)]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/Customer/5

        /// <summary>
        /// Recuperar um item
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item desejado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [AuthorizeFilter(AreaEnum.Customer, ModeEnum.Read)]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(AccountTicket, id);
        }

        // DELETE api/Customer/5

        /// <summary>
        /// Excluir um item
        /// </summary>
        /// <param name="id">Código do item desejado</param>
        /// <returns>Item excluído</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AreaEnum.Customer, ModeEnum.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return await RepositoryService.DeleteAsync(AccountTicket, id);
        }

        // POST api/Customer
        
        /// <summary>
        /// Criar um item
        /// </summary>
        /// <param name="request">Definição do item</param>
        /// <returns>Item criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AreaEnum.Customer, ModeEnum.Create)]
        public async Task<IActionResult> Post
            ([FromBody]CustomerRequest request)
        {
            return await RepositoryService.PostAsync(AccountTicket, request);
        }

        // PUT api/Customer/4

        /// <summary>
        /// Alterar um item
        /// </summary>
        /// <param name="request">Definição do item</param>
        /// <returns>Item alterado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AreaEnum.Customer, ModeEnum.Modify)]
        public async Task<IActionResult> Put
            (int id, [FromBody]CustomerRequest request)
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
                    new CustomerResponse
                    {
                        Id = 1,
                        Name = "Customer A"
                    },
                    new CustomerResponse
                    {
                        Id = 2,
                        Name = "Customer B"
                    }
                };
            }
        }

        private class GetResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new CustomerResponse
                {
                    Id = 1,
                    Name = "Customer A"
                };
            }
        }

        private class PostRequest : IExamplesProvider
        {
            public object GetExamples()
            {
                return new CustomerRequest
                {
                    Name = "Customer C"
                };
            }
        }

        #endregion
    }
}
