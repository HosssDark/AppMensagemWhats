using Domain;
using Functions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using Site.Libraries;
using Site.Models;
using static Functions.Enum;
using static Functions.Constant;
using System.IO;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class DiscenteController : Controller
    {
        private IDiscenteRepository _discenteRep = new DiscenteRepository();
        private ILogRepository _LogRep = new LogRepository();
        private LoginUser _LoginUser;

        public DiscenteController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? StatusId = null)
        {
            try
            {
                var Model = _discenteRep.Grid(Buscar, StatusId);

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
        public IActionResult Adicionar(ViewModel.DiscenteViewModel Model)
        {
            try
            {
                #region Validacao

                if (Model.Discente.DataNascimento == null)
                    ModelState.AddModelError("Discente_DataNascimento", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Nome))
                    ModelState.AddModelError("Discente_Nome", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Matricula))
                    ModelState.AddModelError("Matricula_Aluno", Constant.MsgObrigatorio);
                else
                {
                    if (_discenteRep.MatriculaJaCadastrado(Model.Discente.Matricula))
                        ModelState.AddModelError("Matricula_Aluno", "Matrícula já cadastrado");
                }

                if (!string.IsNullOrEmpty(Model.Discente.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Discente.Email))
                        ModelState.AddModelError("Discente_Email", "Email Inválido!");

                    if (_discenteRep.EmailJaCadastrado(Model.Discente.Email))
                        ModelState.AddModelError("Discente_Email", "Email já cadastrado");
                }
                else
                    ModelState.AddModelError("Discente_Email", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Celular))
                    ModelState.AddModelError("Discente_Celular", Constant.MsgObrigatorio);

                if (Model.Discente.CursoId == 0)
                    ModelState.AddModelError("Discente_CursoId", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    if (Model.File != null)
                    {
                        if (Model.File.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                Model.File.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                Model.Discente.Imagem = string.Format("data:{0};base64,{1}", Model.File.ContentType, Convert.ToBase64String(fileBytes));
                            }
                        }
                    }

                    Model.Discente.DataCadastro = DateTime.Now;
                    Model.Discente.Ativo = true;

                    _discenteRep.Add(Model.Discente);

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
                var Discente = _discenteRep.GetById(Id);

                ViewModel.DiscenteViewModel Model = new ViewModel.DiscenteViewModel()
                {
                    Discente = Discente
                };

                return View(Model);
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
        public IActionResult Alterar(ViewModel.DiscenteViewModel Model)
        {
            try
            {
                #region Validacao

                if (Model.Discente.DataNascimento == null)
                    ModelState.AddModelError("Discente_DataNascimento", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Nome))
                    ModelState.AddModelError("Discente_Nome", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Matricula))
                    ModelState.AddModelError("Matricula_Aluno", Constant.MsgObrigatorio);

                if (!string.IsNullOrEmpty(Model.Discente.Email))
                {
                    if (!FunctionsValidate.ValidateEmail(Model.Discente.Email))
                        ModelState.AddModelError("Discente_Email", "Email Inválido!");
                }
                else
                    ModelState.AddModelError("Discente_Email", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Discente.Celular))
                    ModelState.AddModelError("Discente_Celular", Constant.MsgObrigatorio);

                if (Model.Discente.CursoId == 0)
                    ModelState.AddModelError("Discente_CursoId", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    if (Model.File != null)
                    {
                        if (Model.File.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                Model.File.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                Model.Discente.Imagem = string.Format("data:{0};base64,{1}", Model.File.ContentType, Convert.ToBase64String(fileBytes));
                            }
                        }
                    }

                    _discenteRep.Attach(Model.Discente);

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
                var Discente = _discenteRep.GetById(Id);

                ViewModel.DiscenteViewModel Model = new ViewModel.DiscenteViewModel()
                {
                    Discente = Discente
                };

                return View(Model);
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
                var Model = _discenteRep.GetById(Id);

                if (Model != null)
                {
                    _discenteRep.Inativar(Model);

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