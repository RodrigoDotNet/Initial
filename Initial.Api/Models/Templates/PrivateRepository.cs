using Initial.Api.Models.Database;
using Initial.Api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class PrivateRepository<T> : IPrivateRepository<T>
    {
        protected readonly InitialDatabase _database;

        public PrivateRepository(InitialDatabase database)
        {
            _database = database;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(AccountTicket user)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(AccountTicket user, int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> RemoveAsync(AccountTicket user, int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> DeleteAsync(AccountTicket user, int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task SaveAsync(AccountTicket user, T model)
        {
            throw new NotImplementedException();
        }
    }
}
