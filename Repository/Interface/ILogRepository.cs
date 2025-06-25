using Domain;
using System;

namespace Repository
{
    public interface ILogRepository : IRepositoryBase<Log>, IDisposable
    {
    }
}