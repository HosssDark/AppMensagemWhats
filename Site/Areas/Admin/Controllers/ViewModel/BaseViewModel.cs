using System.ComponentModel.DataAnnotations;
using System;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            DataCadastro = DateTime.Now;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }
    }
}