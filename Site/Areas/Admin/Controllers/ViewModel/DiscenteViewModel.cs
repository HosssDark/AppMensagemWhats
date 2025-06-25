using Domain;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Site.Areas.Admin.Controllers.ViewModel
{
    public class DiscenteViewModel
    {
        public Discente Discente { get; set; }
        public string Image { get; set; }

        [Display(Name = "Imagem (700x500)")]
        public IFormFile File { get; set; }
    }
}