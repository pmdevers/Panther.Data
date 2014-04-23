using System;
using System.Collections.Generic;
using System.Data;

namespace Panther.Data.PetaPoco
{
    public class DbContextManager
    {
        private static Dictionary<string, IDbContextBuilder<IDbContext>> DbContextBuilders;
        private static readonly object SyncLock = new object();
        private static IDbContextStorage Storage { get; set; }

        public static readonly string DefaultConnectionStringName = "Default";
        public static IDbContext Current
        {
            get { return CurrentFor(DefaultConnectionStringName); }
        }

        public static IDbContext CurrentFor(string key)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if(Storage == null)
                throw new ArgumentNullException("IDbContextStorage");
            
            IDbContext context = null;
            lock(SyncLock)
            {
                if(!DbContextBuilders.ContainsKey(key))
                {
                    throw new ApplicationException("An DbContextBuilder does not exist with a key of " + key);
                }

                context = Storage.GetDbContextForKey(key);

                if(context == null)
                {
                    context = DbContextBuilders[key].BuildDbContext();
                    Storage.SetDbContextForKey(key,context);
                }
            }
            return context;
        }

        public static void Init()
        {
            DbContextBuilders = new Dictionary<string, IDbContextBuilder<IDbContext>>();
            Init(DefaultConnectionStringName);
        }

        public static void Init(string connectionStringName)
        {
            DbContextBuilders = new Dictionary<string, IDbContextBuilder<IDbContext>>();

            lock (SyncLock)
            {
                DbContextBuilders.Add(connectionStringName,
                    new DbContextBuilder<IDbContext>(connectionStringName));
            }
        }

        public static void CloseAllDbContexts()
        {
            foreach (IDbContext ctx in Storage.GetAllDbContexts())
            {
                if (((Database)ctx.Context).Connection != null && ((Database)ctx.Context).Connection.State == ConnectionState.Open)
                {
                    ((Database)ctx.Context).Connection.Close();
                }
            }
        }

        public static void InitStorage(IDbContextStorage storage)
        {
            if(storage == null)
                throw new ArgumentException("storage");
            
            Storage = storage;
        }
    }
}
