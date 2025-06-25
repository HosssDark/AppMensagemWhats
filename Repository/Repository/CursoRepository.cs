using Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Repository
{
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public IEnumerable<CursoViewModel> Grid(string Buscar, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            var Model = (from cusro in this.Get(a => a.Ativo)
                         select new CursoViewModel
                         {
                             Id = cusro.Id,
                             Descricao = cusro.Descricao,
                             DataCadastro = cusro.DataCadastro,
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Descricao.ToLower().Contains(Buscar.ToLower()));

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro.Date >= DataInicial?.Date);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro.Date <= DataFinal?.Date);

            #endregion

            return Model;
        }

        public void Inativar(Curso Model)
        {
            if (Model != null)
            {
                Model.DataInativacao = DateTime.Now;
                Model.Ativo = false;

                Attach(Model);
            }
        }
    }
}