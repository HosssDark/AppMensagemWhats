using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ICursoRepository : IRepositoryBase<Curso>, IDisposable
    {
        IEnumerable<CursoViewModel> Grid(string Buscar, DateTime? DataInicial = null, DateTime? DataFinal = null);

        void Inativar(Curso Model);
    }
}