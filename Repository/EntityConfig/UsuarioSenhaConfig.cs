using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UsuarioSenhaConfig : IEntityTypeConfiguration<UsuarioSenha>
    {
        public void Configure(EntityTypeBuilder<UsuarioSenha> builder)
        {
            builder.HasKey(a => a.UsuarioId);

            builder.ToTable("usuario_senha");
        }
    }
}