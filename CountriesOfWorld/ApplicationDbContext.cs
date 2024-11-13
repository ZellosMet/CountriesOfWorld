using Microsoft.EntityFrameworkCore;

namespace CountriesOfWorld
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Сountry> Сountries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Path = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Path).AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");         
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
