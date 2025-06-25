using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Site.Libraries;
using Site.Models;
using static Functions.Enum;
using static Site.Notification;

namespace Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Logged]
    public class HomeController : Controller
    {
        private LoginUser _LoginUser;

        private IUsuarioRepository _userRep = new UsuarioRepository();

        public HomeController(LoginUser loginUser)
        {
            _LoginUser = loginUser;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DashBoard");
        }

        public IActionResult UserImage()
        {
            try
            {
                ViewBag.Image = _LoginUser.GetUser().Imagem;

                return View(_LoginUser.GetUser());
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult DashBoard()
        {
            ILogRepository _LogRep = new LogRepository();

            _LogRep.Add(new Log
            {
                Descricao = string.Format("{0}", _LoginUser.GetUser()),
                Origem = "DashBord",
                UsuarioId = 1
            });

            return View();
        }

        public IActionResult DashCards()
        {
            IMensagemWhatsRepository _mensagemRep = new MensagemWhatsRepository();
            ICursoRepository _cursoRep = new CursoRepository();
            IDiscenteRepository _dicRep = new DiscenteRepository();
            IUsuarioRepository _usuarioRep = new UsuarioRepository();

            ViewModel.DashCardsViewModel Model = new ViewModel.DashCardsViewModel
            {
                MensagemEnviadaTotal = _mensagemRep.Get(a => a.Enviado).Count(),
                MensagemNaoEnviadaTotal = _mensagemRep.Get(a => !a.Enviado).Count(),
                MensagemTotal = _mensagemRep.GetAll().Count(),
                CursosTotal = _cursoRep.Get().Count(),
                DiscentesTotal = _dicRep.Get(a => a.StatusId == 1).Count(),
                UsuarioTotal = _usuarioRep.Get(a => a.StatusId == 1).Count()
            };

            return View(Model);
        }

        public IActionResult DashMensagem()
        {
            try
            {
                IMensagemWhatsRepository _mensagemRep = new MensagemWhatsRepository();

                return View(_mensagemRep.GetAll());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Exit()
        {
            _LoginUser.Exit();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LeftBar()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Notifications()
        {
            List<NotificationList> List = new List<NotificationList>();

            if (TempData.ContainsKey("Success"))
            {
                TempData.TryGetValue("Success", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = TypeMessage.Success,
                        Message = item.ToString()
                    });
                }
            }

            if (TempData.ContainsKey("Error"))
            {
                TempData.TryGetValue("Error", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = TypeMessage.Error,
                        Message = item.ToString()
                    });
                }
            }

            if (TempData.ContainsKey("Info"))
            {
                TempData.TryGetValue("Info", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = TypeMessage.Info,
                        Message = item.ToString()
                    });
                }
            }

            if (TempData.ContainsKey("Warning"))
            {
                TempData.TryGetValue("Warning", out object value);
                var Message = value as IEnumerable<string> ?? Enumerable.Empty<string>();

                foreach (var item in TempData.Values)
                {
                    List.Add(new NotificationList
                    {
                        Type = TypeMessage.Warning,
                        Message = item.ToString()
                    });
                }
            }

            return Json(new { List = List });
        }
    }
}