using System.ComponentModel.DataAnnotations;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class UsuarioViewModel : BaseViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Tipo Usuário")]
        [Required(ErrorMessage = "Obrigatório")]
        public string TipoUsuario { get; set; }

        public string Imagem { get; set; }

        public bool Ativo { get; set; }
    }
}
