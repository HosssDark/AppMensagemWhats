using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class PerfilViewModel : BaseViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public UsuarioSenhaViewModel Senha { get; set; }

        [Display(Name = "Imagem (550x550)")]
        public IFormFile File { get; set; }

        [Display(Name = "Imagem Capa (1500x572)")]
        public IFormFile FileCover { get; set; }
    }
}