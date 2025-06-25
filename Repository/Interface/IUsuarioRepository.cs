using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>, IDisposable
    {
        Usuario VerificationEmail(string Email);

        IEnumerable<UsuarioViewModel> Grid(string Buscar, string TiposUsuario = "", int? StatusId = null);

        void Inativar(Usuario Model);

        bool EmailJaCadastrado(string Email);
    }
}