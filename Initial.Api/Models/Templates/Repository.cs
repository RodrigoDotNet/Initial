using Initial.Api.Models.Database;
using Initial.Api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected readonly InitialDatabase _database;

        public Repository(InitialDatabase database)
        {
            _database = database;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task SaveAsync(T model)
        {
            throw new NotImplementedException();
        }
    }
}
