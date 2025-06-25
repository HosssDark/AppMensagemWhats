using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MensagemWhats : BaseEntity
    {
        public bool Ativo { get; set; }

        public DateTime? DataInativacao { get; set; }

        [Display(Name = "Assunto")]
        public string Assunto { get; set; }

        [Display(Name = "Mensagem")]
        public string Mensagem { get; set; }

        [Display(Name = "Enviado")]
        public bool Enviado { get; set; }

        [Display(Name = "Data Envio")]
        public DateTime? DataEnvio { get; set; }

        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        [Display(Name = "Curso")]
        public int CursoId { get; set; }

        [Display(Name = "Universidade")]
        public int UniversidadeId { get; set; }
    }
}