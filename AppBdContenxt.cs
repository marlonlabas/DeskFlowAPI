using DeskFlowAPI.Entidades;
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
                c.HasMany(c => c.Chamados)
                .WithOne(ch => ch.Categoria)
                .HasForeignKey(ch => ch.CategoriaId);
            });
            
        }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}