using System;

namespace Panther.Data.PetaPoco
{
    public class PetaPocoContext : IDbContext
    {
        private readonly Database _db;

        public PetaPocoContext(Database database)
        {
            _db = database;
        }

        public Object Context { get { return _db; } }

        public Database Db { get { return _db; } }
    }
}
