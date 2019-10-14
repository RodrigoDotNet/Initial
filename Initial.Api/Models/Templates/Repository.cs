using Initial.Api.Models.Database;

namespace Initial.Api.Models.Templates
{
    public abstract class Repository
    {
        protected readonly InitialDatabase _database;

        public Repository(InitialDatabase database)
        {
            _database = database;
        }
    }
}
