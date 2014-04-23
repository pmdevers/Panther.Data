using System.Collections.Generic;

namespace Panther.Data
{
    public interface IDbContextStorage
    {
        IDbContext GetDbContextForKey(string key);
        void SetDbContextForKey(string key, IDbContext dbContext);
        IEnumerable<IDbContext> GetAllDbContexts();
    }
}
