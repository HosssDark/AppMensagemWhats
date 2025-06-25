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
    public class CursoController : Controller
    {
        private ICursoRepository _cursoRep = new CursoRepository();
        private ILogRepository _LogRep = new LogRepository();
        private LoginUser _LoginUser;

        public CursoController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _cursoRep.Grid(Buscar, DataInicial, DataFinal);

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
        public IActionResult Adicionar(Curso Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Descricao))
                    ModelState.AddModelError("Descricao", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.Ativo = true;

                    _cursoRep.Add(Model);

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
                return View(_cursoRep.GetById(Id));
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
        public IActionResult Alterar(Curso Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Descricao))
                    ModelState.AddModelError("Descricao", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    _cursoRep.Attach(Model);

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
                return View(_cursoRep.GetById(Id));
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
                var Model = _cursoRep.GetById(Id);

                if (Model != null)
                {
                    _cursoRep.Inativar(Model);

                    return Json(new ResultViewModel(TypeResult.Ok, TypeMessage.Success, Constant.MsgRegistroInativado));
                }

                return Json(new ResultViewModel(TypeResult.Error, TypeMessage.Error, Constant.MsgRegistroNaoEncontrado));
            }
            catch (Exception Error)
            {
                Log(Error);
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
