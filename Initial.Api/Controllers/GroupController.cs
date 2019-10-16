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
    [AuthorizeFilter]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class GroupController 
        : ControllerServiceBase<IGroupService>
    {
        public GroupController
            (IGroupService service)
            : base(service) { }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllResponse))]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return await RepositoryService.GetAllAsync(AccountTicket);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            return await RepositoryService.GetAsync(AccountTicket, id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
        [AuthorizeFilter(AreaEnum.Group, ModeEnum.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            return await RepositoryService.DeleteAsync(AccountTicket, id);
        }

        // POST api/values/5
        [HttpPost]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
        [AuthorizeFilter(AreaEnum.Group, ModeEnum.Create)]
        public async Task<IActionResult> Post
            ([FromBody]GroupRequest request)
        {
            return await RepositoryService.PostAsync(AccountTicket, request);
        }

        // POST api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetResponse))]
        [SwaggerRequestExample(typeof(GroupRequest), typeof(PostRequest))]
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
