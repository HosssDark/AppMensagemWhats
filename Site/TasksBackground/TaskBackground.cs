using Microsoft.Extensions.Hosting;
using Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Repository.Interface;
using System.Net;
using Domain;

namespace Site
{
    public class TaskBackground : IHostedService, IDisposable
    {
        private Timer _timerEnviarMensagemWhatsapp;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerEnviarMensagemWhatsapp = new Timer(EnviarMensagemWhatsapp, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void EnviarMensagemWhatsapp(object state)
        {
            IMensagemWhatsRepository _mensagemRep = new MensagemWhatsRepository();
            ICursoRepository _cursoRep = new CursoRepository();
            IDiscenteRepository _discenteRep = new DiscenteRepository();
            IUniversidadeRepository _universidadeRep = new UniversidadeRepository();
            ILogRepository _LogRep = new LogRepository();

            foreach (var mensagem in _mensagemRep.GetAll())
            {
                if (mensagem != null)
                {
                    if (mensagem.DataEnvio?.Date == DateTime.Now.Date && !mensagem.Enviado)
                    {
                        var unemat = _universidadeRep.GetFirst();

                        var bearer = string.Format("Bearer {0}", unemat.BearerToken);

                        using (var httpClient = new HttpClient())
                        {
                            using (var request = new HttpRequestMessage(new HttpMethod("POST"), unemat.Url))
                            {
                                request.Headers.TryAddWithoutValidation("Authorization", bearer);

                                var curso = _cursoRep.GetById(mensagem.CursoId);
                                var discentes = _discenteRep.Get(a => a.CursoId == curso.Id);

                                foreach (var discente in discentes)
                                {
                                    string content = "{ \"messaging_product\": \"whatsapp\", \"to\": \"celular\", \"type\": \"template\", \"template\": { \"name\": \"hello_world\", \"language\": { \"code\": \"en_US\" } } }";

                                    content = content.Replace("celular", string.Format("55{0}", discente.Celular));

                                    request.Content = new StringContent(content);
                                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                                    var response = httpClient.SendAsync(request).GetAwaiter().GetResult();

                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        mensagem.Enviado = true;

                                        _mensagemRep.Attach(mensagem);
                                    }
                                    else
                                    {
                                        _LogRep.Add(new Log
                                        {
                                            Descricao = response.ToString(),
                                            Origem = "TaskBackground",
                                            UsuarioId = 0
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timerEnviarMensagemWhatsapp?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timerEnviarMensagemWhatsapp?.Dispose();
        }
    }
}