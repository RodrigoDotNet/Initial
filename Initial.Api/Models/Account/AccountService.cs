using Initial.Api.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Initial.Api.Models
{
    public class AccountService : IAccountService
    {
        protected readonly IAccountRepository _repository;

        protected readonly AppSettings _appSettings;

        public AccountService(IAccountRepository repository, AppSettings appSettings)
        {
            _repository = repository;
            _appSettings = appSettings;
        }

        public async Task<IActionResult> Login(AccountLoginRequest request)
        {
            var passwordHash = CryptoHelper.Guid(request.Password);

            var model = await _repository
                .GetByEmailPasswordAsync(request.Email, passwordHash);

            if (model == null) return new NotFoundResult();

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.JWT_Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Name.ToString()),
                    new Claim(ClaimTypes.Email, model.Email.ToString()),
                    new Claim(ClaimTypes.Sid, model.PublicId.ToString())
                }),

#warning Definir quantidade de dias via AppSettings.
                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new AccountLoginResponse
            {
                Email = model.Email,
                Id = model.Id,
                Name = model.Name,
                PublicId = model.PublicId,
                Token = tokenHandler.WriteToken(token)
            };

            return new OkObjectResult(response);
        }

        public bool IsValid(Guid publicId, out AccountTicket ticket)
        {
            ticket = null;

            // The Result property blocks the calling thread until the task finishes.
            // Ref.: https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-return-a-value-from-a-task

            var model = _repository
                .GetByPublicIdAsync(publicId)
                .Result;

            if (model == null) return false;

            ticket = new AccountTicket
            {
                Email = model.Email,
                Id = model.Id,
                Name = model.Name,
                PrivateId = model.PrivateId,
                PublicId = model.PublicId,
            };

            return true;
        }
    }
}
