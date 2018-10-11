using TimeShare.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace jobhunting
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeShareContext>
    {
        public TimeShareContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TimeShareContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //configuration["Data:Products:ConnectionString"];
                //

            builder.UseSqlServer(connectionString);

            return new TimeShareContext(builder.Options);
        }
    }
}
