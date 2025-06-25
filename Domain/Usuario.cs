using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Usuario : BaseEntity
    {
        public bool Ativo { get; set; }

        public DateTime? DataInativacao { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Tipo Usuário")]
        public string TipoUsuario { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public string Imagem { get; set; }
    }
}