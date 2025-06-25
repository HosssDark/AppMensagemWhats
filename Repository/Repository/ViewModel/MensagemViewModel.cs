using System;

namespace Repository
{
    public class MensagemViewModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Enviado { get; set; }
        public DateTime? DataEnvio { get; set; }
        public int CursoId { get; set; }
        public string Curso { get; set; }
    }
}