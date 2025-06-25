using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Universidade : BaseEntity
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string BearerToken { get; set; }
        public string Url { get; set; }
    }
}