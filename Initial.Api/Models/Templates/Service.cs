using Initial.Api.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class Service<M, RQ, RS> : IService<RQ, RS>
        where M : class
        where RQ : class
        where RS : class
    {
        protected readonly IRepository<M> _repository;

        public Service(IRepository<M> repository)
        {
            _repository = repository;
        }

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
                return new ConflictResult();
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
                return new ConflictResult();
            }
        }

        public virtual async Task<IActionResult> PostAsync(AccountTicket user, RQ request)
        {
            if (request == null)
                return new BadRequestResult();

            try
            {
                var model = Parse(user, request);

                await _repository.SaveAsync(model);

                if (model != null)
                {
                    return new OkObjectResult(Parse(model));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public virtual async Task<IActionResult> PutAsync(AccountTicket user, int id, RQ request)
        {
            if (request == null)
                return new BadRequestResult();

            try
            {
                var model = await _repository.GetAsync(id);

                if (model != null)
                {
                    Merge(user, model, request);

                    await _repository.SaveAsync(model);

                    return new OkObjectResult(Parse(model));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> RemoveAsync(int id)
        {
            try
            {
                var item = await _repository.RemoveAsync(id);

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
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> DeleteAsync(AccountTicket user, int id)
        {
            try
            {
                var model = await _repository.GetAsync(id);

                if (model != null)
                {
                    Delete(user, model);

                    await _repository.SaveAsync(model);

                    return new OkObjectResult(Parse(model));
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch
            {
                return new ConflictResult();
            }
        }

        protected virtual RS Parse(M model)
        {
            throw new NotImplementedException();
        }

        protected virtual M Parse(AccountTicket user, RQ request)
        {
            throw new NotImplementedException();
        }

        protected virtual void Merge(AccountTicket user, M model, RQ request)
        {
            throw new NotImplementedException();
        }

        protected virtual void Delete(AccountTicket user, M model)
        {
            throw new NotImplementedException();
        }
    }
}
