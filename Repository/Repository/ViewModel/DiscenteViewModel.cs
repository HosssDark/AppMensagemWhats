using System;

namespace Repository
{
    public class DiscenteViewModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string StatusIcon { get; set; }
        public string Image { get; set; }
    }
}