using System;

using Panther.Data.PetaPoco;

namespace Panther.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Database Db { get; }
    }
}
