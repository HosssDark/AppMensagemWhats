namespace Domain
{
    public class UsuarioSenha : BaseEntity
    {
        public int UsuarioId { get; set; }
        public string Senha { get; set; }
        public string Guid { get; set; }
    }
}