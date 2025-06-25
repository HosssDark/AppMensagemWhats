using Functions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Libraries;
using System;
using Domain;
using Site.Models;
using static Functions.Enum;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRep = new UsuarioRepository();
        private ILogRepository _LogRep = new LogRepository();
        private LoginUser _LoginUser;

        public UsuarioController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, string TiposUsuario = "", int? StatusId = null)
        {
            try
            {
                var Model = _usuarioRep.Grid(Buscar, TiposUsuario, StatusId);

                return View(Model);
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = Constant.MsgErroObterRegistro;
                return View();
            }
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Usuario Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.NomeCompleto))
                    ModelState.AddModelError("NomeCompleto", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.TipoUsuario))
                    ModelState.AddModelError("TipoUsuario", Constant.MsgObrigatorio);

                if (!string.IsNullOrEmpty(Model.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");

                    if (_usuarioRep.EmailJaCadastrado(Model.Email))
                        ModelState.AddModelError("Email", "Email já cadastrado");
                }
                else
                    ModelState.AddModelError("Email", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.Ativo = true;

                    _usuarioRep.Add(Model);

                    TempData["Success"] = Constant.MsgGravadoSucesso;

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = Constant.MsgErroTentarGravarRegistro;
                return View(Model);
            }
        }

        public IActionResult Alterar(int Id)
        {
            try
            {
                return View(_usuarioRep.GetById(Id));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Descricao = Error.Message,
                    Origem = "UsuarioController",
                    UsuarioId = 1
                });

                #endregion

                TempData["Error"] = Constant.MsgRegistroNaoEncontrado;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Usuario Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Nome))
                    ModelState.AddModelError("Nome", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.NomeCompleto))
                    ModelState.AddModelError("NomeCompleto", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.TipoUsuario))
                    ModelState.AddModelError("TipoUsuario", Constant.MsgObrigatorio);

                if (!string.IsNullOrEmpty(Model.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Email))
                        ModelState.AddModelError("Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("Email", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    _usuarioRep.Attach(Model);

                    TempData["Success"] = Constant.MsgAlteradoSucesso;

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = Constant.MsgErroTentarAlterarRegistro;
                return View(Model);
            }
        }

        public IActionResult Detalhes(int Id)
        {
            try
            {
                return View(_usuarioRep.GetById(Id));
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = Constant.MsgRegistroNaoEncontrado;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Excluir(int Id)
        {
            try
            {
                var Model = _usuarioRep.GetById(Id);

                if (Model != null)
                {
                    _usuarioRep.Inativar(Model);

                    return Json(new ResultViewModel(TypeResult.Ok, TypeMessage.Success, Constant.MsgRegistroInativado));
                }

                return Json(new ResultViewModel(TypeResult.Error, TypeMessage.Error, Constant.MsgRegistroNaoEncontrado));
            }
            catch (Exception Error)
            {
                #region + Log

                _LogRep.Add(new Log
                {
                    Descricao = Error.Message,
                    Origem = "UsuarioController",
                    UsuarioId = 1
                });

                #endregion

                TempData["Error"] = Constant.MsgErroTentarExcluir;
                return Json(new { Result = "Erro" });
            }
        }

        public void Log(Exception Error)
        {
            _LogRep.Add(new Log
            {
                Descricao = Error.Message,
                Origem = "UsuarioController",
                UsuarioId = _LoginUser.GetUser().Id
            });
        }
    }
}
