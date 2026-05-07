using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Complect> Complects { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceName> DeviceNames { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<DeviceProvider> DeviceProviders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    ConfigManager configManager = new();
        //    Config config = configManager.GetConfig();
        //    //string connectionString = $"server = 192.168.1.18; user = root; password = aN271828; database = hardware";
        //    string connectionString = $"server = {config.Server}; user = {config.User}; password = {config.Password}; database = {config.Database}";
        //    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
                        .HasMany(b => b.Cabinets)
                        .WithOne(c => c.Building)
                        .HasForeignKey(c => c.BuildingId);

            modelBuilder.Entity<Cabinet>()
                        .HasMany(c => c.Complects)
                        .WithOne(c => c.Cabinet)
                        .HasForeignKey(c => c.CabinetId);

            modelBuilder.Entity<Complect>()
                        .HasMany(c => c.Devices)
                        .WithOne(d => d.Complect)
                        .HasForeignKey(d => d.ComplectId);

            modelBuilder.Entity<DeviceType>()
                        .HasMany(dt => dt.DeviceNames)
                        .WithOne(dn => dn.DeviceType)
                        .HasForeignKey(dn => dn.DeviceTypeId);

            modelBuilder.Entity<DeviceName>()
                        .HasMany(dn => dn.Devices)
                        .WithOne(d => d.DeviceName)
                        .HasForeignKey(d => d.DeviceNameId);

            modelBuilder.Entity<DeviceProvider>()
                        .HasMany(dp => dp.Devices)
                        .WithOne(d => d.DeviceProvider)
                        .HasForeignKey(d => d.DeviceProviderId);

            modelBuilder.Entity<Device>()
                        .HasIndex(d => d.Serial)
                        .IsUnique();
        }
    }
}
