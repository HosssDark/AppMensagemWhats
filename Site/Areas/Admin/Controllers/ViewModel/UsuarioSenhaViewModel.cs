using System.ComponentModel.DataAnnotations;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class UsuarioSenhaViewModel : BaseViewModel
    {
        public int UsuarioId { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmaSenha { get; set; }
    }
}
