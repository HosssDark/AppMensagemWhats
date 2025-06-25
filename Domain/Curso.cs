using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Curso : BaseEntity
    {

        public bool Ativo { get; set; }

        public DateTime? DataInativacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}