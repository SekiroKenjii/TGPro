using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TGPro.Data.EF
{
    public class TGProDbContextFactory : IDesignTimeDbContextFactory<TGProDbContext>
    {
        public TGProDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TGProDb");

            var optionsBuilder = new DbContextOptionsBuilder<TGProDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TGProDbContext(optionsBuilder.Options);
        }
    }
}
