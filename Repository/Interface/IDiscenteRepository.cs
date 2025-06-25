using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IDiscenteRepository : IRepositoryBase<Discente>, IDisposable
    {
        IEnumerable<DiscenteViewModel> Grid(string Buscar, int? StatusId = null);

        void Inativar(Discente Model);

        bool EmailJaCadastrado(string Email);

        bool MatriculaJaCadastrado(string Matricula);
    }
}