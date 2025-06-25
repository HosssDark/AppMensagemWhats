using Domain;
using Functions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Areas.Admin.Controllers.ViewModel;
using Site.Libraries;
using System;
using System.IO;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class PerfilController : Controller
    {
        private IUsuarioRepository _usuarioRep = new UsuarioRepository();
        private LoginUser _LoginUser;

        public PerfilController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            Usuario usuario = _LoginUser.GetUser();

            var Model = new PerfilViewModel
            {
                Usuario = new ViewModel.UsuarioViewModel
                {
                    Id = usuario.Id,
                    DataCadastro = usuario.DataCadastro,
                    Nome = usuario.Nome,
                    NomeCompleto = usuario.NomeCompleto,
                    TipoUsuario = usuario.TipoUsuario,
                    Email = usuario.Email,
                    Imagem = usuario.Imagem
                },
                Senha = new UsuarioSenhaViewModel
                {
                    UsuarioId = _LoginUser.GetUser().Id,
                    Email = _LoginUser.GetUser().Email
                }
            };

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(PerfilViewModel Model)
        {
            #region Validacao

            if (string.IsNullOrEmpty(Model.Usuario.Nome))
                ModelState.AddModelError("Usuario_Nome", Constant.MsgObrigatorio);

            if (Model.Senha.Senha != Model.Senha.ConfirmaSenha)
                ModelState.AddModelError("Senha_ConfirmaSenha", Constant.MsgSenhaDiferentes);

            #endregion

            if (ModelState.IsValid)
            {
                var usuario = _usuarioRep.GetById(Model.Usuario.Id);

                if (Model.File != null)
                {
                    if (Model.File.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            Model.File.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            usuario.Imagem = string.Format("data:{0};base64,{1}", Model.File.ContentType, Convert.ToBase64String(fileBytes));
                        }
                    }
                }

                usuario.Nome = Model.Usuario.Nome;
                usuario.NomeCompleto = Model.Usuario.NomeCompleto;

                _usuarioRep.Attach(usuario);

                if (!string.IsNullOrEmpty(Model.Senha.Senha))
                    _usuarioRep.AlteraSenha(Model.Usuario.Id, Model.Senha.Senha);

                Model = AtualizaUsuario(Model.Usuario.Id);

                _LoginUser.SetUser(_usuarioRep.GetById(Model.Usuario.Id));

                TempData["Success"] = Constant.MsgAlteradoSucesso;
            }

            return View("Index", Model);
        }

        public PerfilViewModel AtualizaUsuario(int Id)
        {
            var usuario = _usuarioRep.GetById(Id);

            return new PerfilViewModel
            {
                Usuario = new ViewModel.UsuarioViewModel
                {
                    Id = usuario.Id,
                    DataCadastro = usuario.DataCadastro,
                    Nome = usuario.Nome,
                    NomeCompleto = usuario.NomeCompleto,
                    TipoUsuario = usuario.TipoUsuario,
                    Email = usuario.Email,
                    Imagem = usuario.Imagem
                },
                Senha = new UsuarioSenhaViewModel
                {
                    UsuarioId = _LoginUser.GetUser().Id,
                    Email = _LoginUser.GetUser().Email
                }
            };
        }
    }
}