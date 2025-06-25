using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class UniversidadeConfig : IEntityTypeConfiguration<Universidade>
    {
        public void Configure(EntityTypeBuilder<Universidade> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("universidade");
        }
    }
}