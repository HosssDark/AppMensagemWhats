using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public override Log Add(Log Entity)
        {
            Entity.DataAlteracao = DateTime.Now;

            return base.Add(Entity);
        }

        public override List<Log> AddAll(List<Log> List)
        {
            foreach (var item in List)
            {
                item.DataAlteracao = DateTime.Now;
            }

            return base.AddAll(List);
        }
    }
}
