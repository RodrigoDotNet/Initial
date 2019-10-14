using Initial.Api.Models.Database;
using Initial.Api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Initial.Api.Models.Templates
{
    public abstract class PublicRepository<T> : IPublicRepository<T>
    {
        protected readonly InitialDatabase _database;

        public PublicRepository(InitialDatabase database)
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
    }
}
