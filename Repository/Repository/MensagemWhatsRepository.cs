using Domain;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Repository
{
    public class MensagemWhatsRepository : RepositoryBase<MensagemWhats>, IMensagemWhatsRepository
    {
        public IEnumerable<MensagemViewModel> Grid(string Buscar, int? CursoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            ICursoRepository _cursoRep = new CursoRepository();

            var Model = (from mensagem in this.Get(a => a.Ativo)
                         join curso in _cursoRep.GetAll() on mensagem.CursoId equals curso.Id
                         select new MensagemViewModel
                         {
                             Id = mensagem.Id,
                             Assunto = mensagem.Assunto,
                             Mensagem = mensagem.Mensagem,
                             Enviado = mensagem.Enviado.Equals(true)? "Sim": "Não",
                             DataEnvio = mensagem.DataEnvio,
                             CursoId = curso.Id,
                             Curso = curso.Descricao,
                             DataCadastro = mensagem.DataCadastro,
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Assunto.ToLower().Contains(Buscar.ToLower()) || a.Mensagem.ToLower().Contains(Buscar.ToLower()));

            if (DataInicial != null)
                Model = Model.Where(a => a.DataEnvio?.Date >= DataInicial?.Date);

            if (CursoId != null)
                Model = Model.Where(a => a.CursoId == CursoId);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataEnvio?.Date <= DataFinal?.Date);

            #endregion

            return Model;
        }

        public void Inativar(MensagemWhats Model)
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