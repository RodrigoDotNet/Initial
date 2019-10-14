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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class CustomerController 
        : ControllerServiceBase<ICustomerService>
    {
        public CustomerController
            (ICustomerService service)
            : base(service) { }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        [AuthorizeFilter(AccessAreaEnum.Customer, AccessModeEnum.Read)]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [AuthorizeFilter(AccessAreaEnum.Customer, AccessModeEnum.Read)]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(AccountTicket, id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AccessAreaEnum.Customer, AccessModeEnum.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return await RepositoryService.DeleteAsync(AccountTicket, id);
        }

        // POST api/values/5
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AccessAreaEnum.Customer, AccessModeEnum.Create)]
        public async Task<IActionResult> Post
            ([FromBody]CustomerRequest request)
        {
            return await RepositoryService.PostAsync(AccountTicket, request);
        }

        // POST api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(CustomerRequest), typeof(PostRequest))]
        [AuthorizeFilter(AccessAreaEnum.Customer, AccessModeEnum.Modify)]
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
