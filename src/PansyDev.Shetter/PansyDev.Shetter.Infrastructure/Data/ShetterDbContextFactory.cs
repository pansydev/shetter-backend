using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PansyDev.Common.Infrastructure.Utils;

namespace PansyDev.Shetter.Infrastructure.Data
{
    internal class ShetterDbContextFactory : IDesignTimeDbContextFactory<ShetterDbContext>
    {
        public ShetterDbContext CreateDbContext(string[] args)
        {
            var secrets = ConfigurationUtils.GetUserSecrets<ShetterDbContext>();
            var connectionString = secrets.GetConnectionString("Default");

            var builder = new DbContextOptionsBuilder<ShetterDbContext>();
            builder.UseNpgsql(connectionString);

            return new ShetterDbContext(builder.Options);
        }
    }
}