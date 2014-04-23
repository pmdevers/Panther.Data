using System.Collections.Generic;

namespace Panther.Data.PetaPoco
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleDbContextStorage : IDbContextStorage
    {
        private Dictionary<string, IDbContext> _storage = new Dictionary<string, IDbContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleDbContextStorage"/> class.
        /// </summary>
        public SimpleDbContextStorage() { }

        /// <summary>
        /// Returns the db context associated with the specified key or
        /// null if the specified key is not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public IDbContext GetDbContextForKey(string key)
        {
            IDbContext context;
            if (!this._storage.TryGetValue(key, out context))
                return null;
            return context;
        }


        /// <summary>
        /// Stores the db context into a dictionary using the specified key.
        /// If an object context already exists by the specified key, 
        /// it gets overwritten by the new object context passed in.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="context">The object context.</param>
        public void SetDbContextForKey(string key, IDbContext context)
        {
            this._storage.Add(key, context);
        }

        /// <summary>
        /// Returns all the values of the internal dictionary of db contexts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDbContext> GetAllDbContexts()
        {
            return this._storage.Values;
        }
    }
}
