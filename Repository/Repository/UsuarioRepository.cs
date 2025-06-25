using Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public override List<Usuario> AddAll(List<Usuario> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
            }

            return base.AddAll(List);
        }

        public Usuario VerificationEmail(string Email)
        {
            return this.Get(a => a.Email == Email).FirstOrDefault();
        }

        public IEnumerable<UsuarioViewModel> Grid(string Buscar, string TiposUsuario = "", int? StatusId = null)
        {
            IStatusRepository _staRep = new StatusRepository();

            var Model = (from usuario in this.Get(a => a.Ativo)
                         join sta in _staRep.GetAll() on usuario.StatusId equals sta.Id
                         select new UsuarioViewModel
                         {
                             Id = usuario.Id,
                             Nome = usuario.Nome,
                             NomeCompleto = usuario.NomeCompleto,
                             TipoUsuario = usuario.TipoUsuario.Equals("Admin")? "Administrador" : "Estagiário",
                             Tipo = usuario.TipoUsuario,
                             Email = usuario.Email,
                             Status = sta.Descricao,
                             StatusId = usuario.StatusId,
                             DataCadastro = usuario.DataCadastro,
                             Imagem = usuario.Imagem
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()) || a.NomeCompleto.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (!string.IsNullOrEmpty(TiposUsuario))
                Model = Model.Where(a => a.Tipo == TiposUsuario);

            #endregion

            return Model;
        }

        public void Inativar(Usuario Model)
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

        public void AlteraSenha(int Id, string Senha)
        {
            IUsuarioSenhaRepository _usuarioSenhaRep = new UsuarioSenhaRepository();

            var Model = _usuarioSenhaRep.Get(a => a.Id == Id).FirstOrDefault();

            var Usuario = GetById(Id);

            if (Model != null)
            {
                Model.Senha = Functions.Functions.MD5Hash(Senha + Usuario.Email);

                _usuarioSenhaRep.Attach(Model);
            }
        }
    }
}