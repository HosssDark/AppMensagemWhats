using Functions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Libraries;
using System.IO;
using System;
using Domain;
using Site.Models;
using static Functions.Enum;
using static Functions.Constant;

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
                TempData["Error"] = MsgErroObterRegistro;
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
                    ModelState.AddModelError("Descricao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.Ativo = true;

                    _cursoRep.Add(Model);

                    TempData["Success"] = MsgGravadoSucesso;

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = MsgErroTentarGravarRegistro;
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
                TempData["Error"] = MsgRegistroNaoEncontrado;
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
                    ModelState.AddModelError("Descricao", "Obrigatório");

                #endregion

                if (ModelState.IsValid)
                {
                    _cursoRep.Attach(Model);

                    TempData["Success"] = MsgAlteradoSucesso;

                    return RedirectToAction("Index");
                }

                return View(Model);
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = MsgErroTentarAlterarRegistro;
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
                TempData["Error"] = MsgRegistroNaoEncontrado;
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

                    return Json(new ResultViewModel(TypeResult.Ok, TypeMessage.Success, MsgRegistroInativado));
                }

                return Json(new ResultViewModel(TypeResult.Error, TypeMessage.Error, MsgRegistroNaoEncontrado));
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = MsgErroTentarExcluir;
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
