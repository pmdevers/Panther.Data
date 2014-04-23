using System.Configuration;
using System.Data.Common;

namespace Panther.Data.PetaPoco
{
    public class DbContextBuilder<T> : IDbContextBuilder<T> where T : IDbContext
    {
        private readonly DbProviderFactory _factory;
        private readonly ConnectionStringSettings _connectionStringSettings;

        public DbContextBuilder(string connectionStringName)
        {
            _connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            _factory = DbProviderFactories.GetFactory(_connectionStringSettings.ProviderName);
        }

        public T BuildDbContext()
        {
            //var cn = _factory.CreateConnection();
            //cn.ConnectionString = _connectionStringSettings.ConnectionString;
            var db = new Database(_connectionStringSettings.ConnectionString, _connectionStringSettings.ProviderName);

            return (T)(IDbContext)new PetaPocoContext(db);
        }
    }
}
