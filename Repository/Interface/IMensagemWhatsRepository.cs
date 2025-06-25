using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IMensagemWhatsRepository : IRepositoryBase<MensagemWhats>, IDisposable
    {
        IEnumerable<MensagemViewModel> Grid(string Buscar, int? CursoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);

        void Inativar(MensagemWhats Model);
    }
}