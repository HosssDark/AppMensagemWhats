using Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Repository
{
    public class DiscenteRepository : RepositoryBase<Discente>, IDiscenteRepository
    {
        public IEnumerable<DiscenteViewModel> Grid(string Buscar, int? StatusId = null)
        {
            IStatusRepository _staRep = new StatusRepository();

            var Model = (from dc in this.Get(a => a.Ativo)
                         join sta in _staRep.GetAll() on dc.StatusId equals sta.Id
                         select new DiscenteViewModel
                         {
                             Id = dc.Id,
                             Matricula = dc.Matricula,
                             Nome = dc.Nome,
                             Email = dc.Email,
                             Celular = dc.Celular,
                             DataCadastro = dc.DataCadastro,
                             StatusId = dc.StatusId,
                             Status = sta.Descricao,
                             Image = dc.Imagem
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()) || a.Matricula.ToLower().Contains(Buscar.ToLower()) || a.Email.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            #endregion

            return Model;
        }

        public void Inativar(Discente Model)
        {
            if (Model != null)
            {
                Model.DataInativacao = DateTime.Now;
                Model.Ativo = false;

                Attach(Model);
            }
        }

        public bool EmailJaCadastrado(string Email)
        {
            return Get(a => a.Email == Email).Count() > 0 ? true : false;
        }

        public bool MatriculaJaCadastrado(string Matricula)
        {
            return Get(a => a.Matricula == Matricula).Count() > 0 ? true : false;
        }
    }
}