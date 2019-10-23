using Initial.Api.Controllers.Templates;
using Initial.Api.Models;
using Initial.Api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Threading.Tasks;

namespace Initial.Api.Controllers
{
    /// <summary>
    /// Métodos de autenticação (Pública)
    /// </summary>
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]/")]
    [ApiController]
    public class AccountController
        : ControllerServiceBase<IAccountService>
    {
        public AccountController
            (IAccountService service)
            : base(service) { }

        // POST api/Account/Login

        /// <summary>
        /// Método de login
        /// </summary>
        /// <param name="request">Requisição de login</param>
        /// <returns>Informações do usuário e token (JWT)</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AccountLoginResponse), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetLoginResponse))]
        [SwaggerRequestExample(typeof(AccountLoginRequest), typeof(GetLoginRequest))]
        public async Task<IActionResult> Login
            ([FromBody]AccountLoginRequest request)
        {
            return await RepositoryService.Login(request);
        }

        #region Examples

        private class GetLoginResponse : IExamplesProvider
        {
            public object GetExamples()
            {
                return new AccountLoginResponse
                {
                    Id = 1,
                    Name = "User A",
                    Email = Examples.Email,
                    PublicId = Guid.Empty,
                    Token = Examples.Token
                };
            }
        }

        private class GetLoginRequest : IExamplesProvider
        {
            public object GetExamples()
            {
                return new AccountLoginRequest
                {
                    Email = "user@enterprisetest.com",
                    Password = "user2019"
                };
            }
        }

        #endregion
    }
}
