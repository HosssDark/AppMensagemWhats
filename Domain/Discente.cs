using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Discente : BaseEntity
    {
        public bool Ativo { get; set; }

        public DateTime? DataInativacao { get; set; }

        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Curso")]
        public int CursoId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Imagem")]
        public string Imagem { get; set; }
    }
}