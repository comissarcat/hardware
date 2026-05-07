using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware
{
    internal class ApplicationContextFactory : IDbContextFactory<ApplicationContext>
    {
        private readonly ConfigManager configManager;

        public ApplicationContextFactory(ConfigManager configManager)
        {
            this.configManager = configManager;
        }

        public ApplicationContext CreateDbContext()
        {
            Config config = configManager.GetConfig();
            string connectionString = $"server = {config.Server}; user = {config.User}; password = {config.Password}; database = {config.Database}";
            DbContextOptionsBuilder<ApplicationContext> optionsBuilder = new();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
