using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //public DbSet<DeskFlowAPI.Models.User> Users { get; set; }
    }
}