using Estate_management.Models;
using Microsoft.EntityFrameworkCore;

namespace Estate_management.Data
{
    public class LandDbContext: DbContext
    { 
            public LandDbContext(DbContextOptions<LandDbContext> options)
                : base(options)
            {
            }

            public DbSet<Estate> Estates { get; set; }
      
    }

}
