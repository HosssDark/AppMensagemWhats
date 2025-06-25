using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.EntityConfig;
using System.IO;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Discente> Discentes { get; set; }
        public DbSet<MensagemWhats> MensagemWhats { get; set; }
        public DbSet<Universidade> Universidade { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioSenha> UsuariosSenha { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

                var Configuration = builder.Build();

                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"), mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Curso>(new CursoConfig());
            modelBuilder.ApplyConfiguration<Discente>(new DiscenteConfig());
            modelBuilder.ApplyConfiguration<MensagemWhats>(new MensagemWhatsConfig());
            modelBuilder.ApplyConfiguration<Universidade>(new UniversidadeConfig());
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioConfig());
            modelBuilder.ApplyConfiguration<UsuarioSenha>(new UsuarioSenhaConfig());
            modelBuilder.ApplyConfiguration<Log>(new LogConfig());
            modelBuilder.ApplyConfiguration<Status>(new StatusConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}