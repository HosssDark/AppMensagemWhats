using System;

namespace Repository
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public string TipoUsuario { get; set; }
        public string Tipo { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Imagem { get; set; }
    }
}