using DeskFlowAPI.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>( c =>
            {
                c.Property(cat => cat.Nome)
                    .IsRequired()
                    .HasMaxLength(100);     

                c.HasMany(cat => cat.Chamados)
                    .WithOne(ch => ch.Categoria)
                    .HasForeignKey(ch => ch.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Chamado>( c =>
            {
                c.Property(ch => ch.Titulo)
                    .IsRequired()
                    .HasMaxLength(150);

                c.Property(ch => ch.Descricao)
                    .IsRequired()
                    .HasMaxLength(500);

                c.Property(ch => ch.SolicitanteNome)
                    .IsRequired()
                    .HasMaxLength(150);

                c.Property(ch => ch.Prioridade)
                    .HasConversion<string>();

                c.Property(ch => ch.Status)
                    .HasConversion<string>();

                c.HasMany(ch => ch.Interacoes)
                    .WithOne(i => i.Chamado)
                    .HasForeignKey(i => i.ChamadoId);
            });
            
             modelBuilder.Entity<Interacao>( c =>
             {
                c.Property(i => i.Autor)
                    .IsRequired()
                    .HasMaxLength(150);

                c.Property(i => i.Mensagem)
                    .IsRequired()
                    .HasMaxLength(1000);
             });
        }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Interacao> Interacoes {get; set; }
    }
}