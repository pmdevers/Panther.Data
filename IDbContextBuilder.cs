namespace Panther.Data
{
    public interface IDbContextBuilder<T> where T : IDbContext
    {
        T BuildDbContext();
    }
}
