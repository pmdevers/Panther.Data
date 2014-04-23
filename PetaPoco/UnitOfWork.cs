namespace Panther.Data.PetaPoco
{
    public class PetaPocoUnitOfWork : IUnitOfWork
    {
        private readonly Transaction _transaction;
        private readonly Database _db;
        
        public PetaPocoUnitOfWork(Database db)
        {
            _db = db;
            _transaction = new Transaction(_db);
        }
        
        public Database Db
        {
            get { return _db; }
        }

        public void Commit()
        {
            _transaction.Complete();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
