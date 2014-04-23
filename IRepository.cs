using System.Collections.Generic;

using Panther.Data.PetaPoco;

namespace Panther.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByKey(object keyValue);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(string sqlCondition, params object[] args);
        TEntity Single(string sqlCondition, params object[] args);
        TEntity First(string sqlCondition, params object[] args);
        TEntity Insert(TEntity entity);
        void Delete(TEntity entity);
        void Delete(string sqlCondition, params object[] args);
        void Update(TEntity entity);
        void Save(TEntity entity);
        Page<TEntity> Get(long pageNumber, long itemsPerPage, string sqlCondition, params object[] args);
        int Count();
        int Count(string sqlCondition, params object[] args);
        IUnitOfWork UnitOfWork { get; }
    }
}
