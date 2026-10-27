using Microsoft.EntityFrameworkCore;
using ServerStudy.Models;

namespace ServerStudy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<GameResult> GameResults { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
    }
}