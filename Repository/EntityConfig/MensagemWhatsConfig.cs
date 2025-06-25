using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class MensagemWhatsConfig : IEntityTypeConfiguration<MensagemWhats>
    {
        public void Configure(EntityTypeBuilder<MensagemWhats> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("mensagens");
        }
    }
}