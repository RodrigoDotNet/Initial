using Initial.Api.Models.Interfaces;
using Initial.Api.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class PublicService<M, RS>
        : Service<IPublicRepository<M>>, IPublicService
        where M : class
        where RS : class
    {
        public PublicService(IPublicRepository<M> repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        public virtual async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var items = await _repository.GetAllAsync();

                if (items != null)
                {
                    return new OkObjectResult(items.Select(Parse));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new ConflictObjectResult(State);
            }
        }

        public virtual async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var item = await _repository.GetAsync(id);

                if (item != null)
                {
                    return new OkObjectResult(Parse(item));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new ConflictObjectResult(State);
            }
        }

        protected virtual RS Parse(M model)
        {
            throw new NotImplementedException();
        }
    }
}
