using Initial.Api.Models.Templates;
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
    public class AccountService : Service<IAccountRepository>, IAccountService
    {
        public AccountService(IAccountRepository repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        public async Task<IActionResult> Login(AccountLoginRequest request)
        {
            if (request == null)
                return new BadRequestResult();

            try
            {
                var passwordHash = CryptoHelper.Guid(request.Password);

                var model = await _repository
                    .GetByEmailPasswordAsync(request.Email, passwordHash);

                if (model == null) return new NotFoundResult();

                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Security.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, model.Name.ToString()),
                    new Claim(ClaimTypes.Email, model.Email.ToString()),
                    new Claim(ClaimTypes.Sid, model.PublicId.ToString())
                    }),

                    Expires = DateTime.UtcNow.AddDays(_appSettings.Security.Expires),

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
            catch
            {
                return new ConflictObjectResult(State);
            }
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
                EnterpriseId = model.EnterpriseId
            };

            return true;
        }

        public async Task<AccessModeEnum> GetAccessAreaMode(AccountTicket ticket, AccessAreaEnum area)
        {
            var response = new AccessModeEnum();

            var lst = await _repository.GetAreaAccess(ticket, (int)area);

            foreach (var item in lst)
            {
                if (item.CanCreate) response |= AccessModeEnum.Create;

                if (item.CanDelete) response |= AccessModeEnum.Delete;

                if (item.CanModify) response |= AccessModeEnum.Modify;

                if (item.CanRead) response |= AccessModeEnum.Read;
            }

            return response;
        }

        public async Task<bool> HasPolicyAccess(AccountTicket ticket, AccessPolicyEnum policy)
        {
            return await _repository.HasPolicyAccess(ticket, (int)policy);
        }
    }
}
