using Microsoft.EntityFrameworkCore;
using TechFixOS.Models.Entidades;

namespace TechFixOS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Cpf).HasMaxLength(14);
                entity.Property(e => e.Ativo).HasDefaultValue(true);
            });

            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Ativo).HasDefaultValue(true);
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Ativo).HasDefaultValue(true);
            });

            modelBuilder.Entity<OrdemServico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(50).HasDefaultValue("Pendente");

                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tecnico)
                      .WithMany()
                      .HasForeignKey(e => e.TecnicoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Servico)
                      .WithMany()
                      .HasForeignKey(e => e.ServicoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Senha).HasMaxLength(64).IsRequired();
                entity.Property(e => e.Perfil).HasMaxLength(20).HasDefaultValue("Usuario");
            });
        }
    }
}
