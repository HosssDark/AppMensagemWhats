using System;

namespace Domain
{
    public class Log
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Origem { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}