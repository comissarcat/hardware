using Hardware.Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace Hardware.Temp
{
	internal class TempApplicatonContext : DbContext
	{
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Complect> Complects { get; set; }
		public DbSet<DeviceType> Types { get; set; }
		public DbSet<DeviceName> Names { get; set; }
		public DbSet<Device> Devices { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = "server = 192.168.1.18; user = root; password = aN271828; database = hardware_new";
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
	}
}
