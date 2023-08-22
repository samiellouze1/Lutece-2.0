using Microsoft.EntityFrameworkCore;
using RecommandationService.Models;
namespace RecommandationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region relationships
            #endregion

            #region doubles
            #endregion
            base.OnModelCreating(builder);
        }
    }
}
