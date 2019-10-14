using Initial.Api.Models.Interfaces;
using Initial.Api.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class PrivateService<M, RQ, RS>
        : Service<IPrivateRepository<M>>, IPrivateService<RQ, RS>
        where M : class
        where RQ : class
        where RS : class
    {
        public PrivateService(IPrivateRepository<M> repository, AppSettings appSettings)
            : base(repository, appSettings) { }

        public virtual async Task<IActionResult> GetAllAsync(AccountTicket user)
        {
            try
            {
                var items = await _repository.GetAllAsync(user);

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

        public virtual async Task<IActionResult> GetAsync(AccountTicket user, int id)
        {
            try
            {
                var item = await _repository.GetAsync(user, id);

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

        public virtual async Task<IActionResult> PostAsync(AccountTicket user, RQ request)
        {
            if (request == null)
                return new BadRequestResult();

            if (!Validate(user, request))
                return new ConflictObjectResult(State);

            try
            {
                var model = Parse(user, request);

                if (!Validate(user, request, model))
                    return new ConflictObjectResult(State);

                await _repository.SaveAsync(user, model);

                return new OkObjectResult(Parse(model));
            }
            catch
            {
                return new ConflictObjectResult(State);
            }
        }

        public virtual async Task<IActionResult> PutAsync(AccountTicket user, int id, RQ request)
        {
            if (request == null)
                return new BadRequestResult();

            if (!Validate(user, request))
                return new ConflictObjectResult(State);

            try
            {
                var model = await _repository.GetAsync(user, id);

                if (model == null)
                    return new NotFoundResult();

                if (!Validate(user, request, model))
                    return new ConflictObjectResult(State);

                Merge(user, model, request);

                await _repository.SaveAsync(user, model);

                return new OkObjectResult(Parse(model));

            }
            catch
            {
                return new ConflictObjectResult(State);
            }
        }

        public virtual async Task<IActionResult> RemoveAsync(AccountTicket user, int id)
        {
            try
            {
                var item = await _repository.RemoveAsync(user, id);

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

        public virtual async Task<IActionResult> DeleteAsync(AccountTicket user, int id)
        {
            try
            {
                var model = await _repository.GetAsync(user, id);

                if (model != null)
                {
                    AuditDelete(user, model);

                    await _repository.SaveAsync(user, model);

                    return new OkObjectResult(Parse(model));
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

        protected virtual M Parse(AccountTicket user, RQ request)
        {
            throw new NotImplementedException();
        }

        protected virtual void Merge(AccountTicket user, M model, RQ request)
        {
            throw new NotImplementedException();
        }

        protected virtual void AuditDelete(AccountTicket user, M model)
        {
            throw new NotImplementedException();
        }

        protected virtual bool Validate(AccountTicket user, RQ request, M model)
        {
            return true;
        }

        protected virtual bool Validate(AccountTicket user, RQ request)
        {
            return true;
        }
    }
}
