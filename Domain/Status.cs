using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Status
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}