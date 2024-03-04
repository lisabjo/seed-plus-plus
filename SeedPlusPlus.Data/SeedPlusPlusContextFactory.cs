using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeedPlusPlus.Data;

public class SeedPlusPlusContextFactory : IDesignTimeDbContextFactory<SeedPlusPlusContext>
{
    public SeedPlusPlusContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SeedPlusPlusContext>();
        optionsBuilder.UseSqlite("Data Source=Seeds.db");

        return new SeedPlusPlusContext(optionsBuilder.Options);
    }
}