using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.EntityConfig
{
    public class DiscenteConfig : IEntityTypeConfiguration<Discente>
    {
        public void Configure(EntityTypeBuilder<Discente> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("discentes");
        }
    }
}