using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BaseEntity
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}