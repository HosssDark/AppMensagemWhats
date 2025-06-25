using Domain;
using System;

namespace Repository
{
    public interface IStatusRepository : IRepositoryBase<Status>, IDisposable
    {
    }
}