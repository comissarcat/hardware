using Hardware.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware
{
	public class ApplicationContext : DbContext
	{
		private static ApplicationContext? instanse = null;
		private ApplicationContext() { }
		public static ApplicationContext Instanse()
		{
			instanse ??= new ApplicationContext();
			return instanse;
		}
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Cabinet> Cabinets { get; set; }
		public DbSet<Complect> Complects { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<DeviceName> DeviceNames { get; set; }
		public DbSet<DeviceType> DeviceTypes { get; set; }
		public DbSet<History> History { get; set; }
		public DbSet<DeviceProvider> DeviceProviders { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = "server = 192.168.1.18; user = root; password = aN271828; database = hardware";
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<History>().Property(h => h.ChangedAt).HasDefaultValue(DateTime.Now);
		}
	}
}
