using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Libraries;
using System;
using Domain;
using Repository.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Site.Models;
using static Functions.Enum;
using static Functions.Constant;
using Functions;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class MensagemController : Controller
    {
        private IMensagemWhatsRepository _mensagemRep = new MensagemWhatsRepository();
        private IUniversidadeRepository _universidadeRep = new UniversidadeRepository();
        private ICursoRepository _cursoRep = new CursoRepository();
        private IDiscenteRepository _discenteRep = new DiscenteRepository();
        private ILogRepository _LogRep = new LogRepository();
        private LoginUser _LoginUser;

        public MensagemController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid(string Buscar, int? CursoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            try
            {
                var Model = _mensagemRep.Grid(Buscar, CursoId, DataInicial, DataFinal);

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
        public IActionResult Adicionar(MensagemWhats Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Assunto))
                    ModelState.AddModelError("Assunto", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Mensagem))
                    ModelState.AddModelError("Mensagem", Constant.MsgObrigatorio);

                if (Model.DataEnvio == null)
                    ModelState.AddModelError("DataEnvio", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Assunto))
                    ModelState.AddModelError("Assunto", Constant.MsgObrigatorio);

                if (Model.CursoId == 0)
                    ModelState.AddModelError("CursoId", Constant.MsgObrigatorio);

                #endregion

                if (ModelState.IsValid)
                {
                    Model.DataCadastro = DateTime.Now;
                    Model.Ativo = true;
                    Model.UsuarioId = _LoginUser.GetUser().Id;
                    Model.UniversidadeId = _universidadeRep.GetFirst().Id;

                    _mensagemRep.Add(Model);

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
                return View(_mensagemRep.GetById(Id));
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = "Registro não encontrado!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(MensagemWhats Model)
        {
            try
            {
                #region Validacao

                if (string.IsNullOrEmpty(Model.Assunto))
                    ModelState.AddModelError("Assunto", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Mensagem))
                    ModelState.AddModelError("Mensagem", Constant.MsgObrigatorio);

                if (Model.DataEnvio == null)
                    ModelState.AddModelError("DataEnvio", Constant.MsgObrigatorio);

                if (string.IsNullOrEmpty(Model.Assunto))
                    ModelState.AddModelError("Assunto", Constant.MsgObrigatorio);

                if (Model.CursoId == 0)
                    ModelState.AddModelError("CursoId", Constant.MsgObrigatorio);
                
                #endregion

                if (ModelState.IsValid)
                {
                    _mensagemRep.Attach(Model);

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
                return View(_mensagemRep.GetById(Id));
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
                var Model = _mensagemRep.GetById(Id);

                if (Model != null)
                {
                    _mensagemRep.Inativar(Model);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EnviarMensagem(int Id)
        {
            try
            {
                var Model = _mensagemRep.GetById(Id);

                if (Model != null)
                {
                    var unemat = _universidadeRep.GetFirst();

                    var bearer = string.Format("Bearer {0}", unemat.BearerToken);

                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), unemat.Url))
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", bearer);

                            var curso = _cursoRep.GetById(Model.CursoId);
                            var discentes = _discenteRep.Get(a => a.CursoId == curso.Id);

                            foreach (var item in discentes)
                            {
                                string content = "{ \"messaging_product\": \"whatsapp\", \"to\": \"celular\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";

                                content = content.Replace("celular", string.Format("55{0}", item.Celular));

                                request.Content = new StringContent(content);
                                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                                var response = httpClient.SendAsync(request).GetAwaiter().GetResult();

                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    Model.Enviado = true;

                                    _mensagemRep.Attach(Model);
                                }
                                else
                                {
                                    _LogRep.Add(new Log
                                    {
                                        Descricao = response.ToString(),
                                        Origem = "UsuarioController",
                                        UsuarioId = _LoginUser.GetUser().Id
                                    });
                                }
                            }
                        }
                    }

                    return Json(new ResultViewModel(TypeResult.Ok, TypeMessage.Success, "Mensagem enviada com sucesso"));
                }

                return Json(new { Result = "Erro", Message = "Erro ao enviar a mensagem!" });
            }
            catch (Exception Error)
            {
                Log(Error);
                TempData["Error"] = "Erro ao tentar Excluir o Registro!";
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