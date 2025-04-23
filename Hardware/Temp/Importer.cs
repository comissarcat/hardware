namespace Hardware.Temp
{
	internal class Importer(ApplicationContext context)
	{
		public void Import()
		{
			var tempContext = new TempApplicatonContext();
			ImportBuildings(tempContext);
			ImportCabinets(tempContext);
			ImportComplects(tempContext);
			ImportDeviceTypes(tempContext);
			ImportDeviceNames(tempContext);
			CreateDeviceProviders();
			ImportDevices(tempContext);
		}

		private void ImportBuildings(TempApplicatonContext tempContext)
		{
			foreach (var building in tempContext.Buildings)
				if (!context.Buildings.Where(b => b.Name == building.Name).Any())
					context.Buildings.Add(new() { Name = building.Name });
			context.SaveChanges();
		}

		private void ImportCabinets(TempApplicatonContext tempContext)
		{
			foreach (var complect in tempContext.Complects)
			{
				var building = context.Buildings.Where(b => b.Name == complect.BuildingEntity.Name).First();
				var name = complect.Cabinet;
				if (!context.Cabinets.Where(c => c.Building == building && c.Name == name).Any())
					context.Cabinets.Add(new() { Building = building, Name = name });
				context.SaveChanges();
			}
		}

		private void ImportComplects(TempApplicatonContext tempContext)
		{
			foreach (var complect in tempContext.Complects)
			{
				var building = context.Buildings.Where(b => b.Name == complect.BuildingEntity.Name).First();
				var cabinet = context.Cabinets.Where(c => c.Building == building && c.Name == complect.Cabinet).First();
				var name = complect.User;
				if (!context.Complects.Where(c => c.Cabinet == cabinet && c.Name == name).Any())
					context.Complects.Add(new() { Cabinet = cabinet, Name = name });
				context.SaveChanges();
			}
		}

		private void ImportDeviceTypes(TempApplicatonContext tempContext)
		{
			foreach (var deviceType in tempContext.Types)
				if (!context.DeviceTypes.Where(d => d.Name == deviceType.Type).Any())
					context.DeviceTypes.Add(new() { Name = deviceType.Type });
			context.SaveChanges();
		}

		private void ImportDeviceNames(TempApplicatonContext tempContext)
		{
			foreach (var deviceName in tempContext.Names)
			{
				var deviceType = context.DeviceTypes.Where(d => d.Name == deviceName.TypeEntity.Type).First();
				var name = deviceName.Name;
				if (!context.DeviceNames.Where(d => d.DeviceType == deviceType && d.Name == name).Any())
					context.DeviceNames.Add(new() { DeviceType = deviceType, Name = name });
				context.SaveChanges();
			}
		}

		private void CreateDeviceProviders()
		{
			if (!context.DeviceProviders.Any())
			{
				context.DeviceProviders.Add(new() { Name = "ИАЦ" });
				context.SaveChanges();
			}
		}

		private void ImportDevices(TempApplicatonContext tempContext)
		{
			foreach (var device in tempContext.Devices)
			{
				var serial = device.Serial_number;
				var inventory = device.Inventory_number;
				var deviceName = context.DeviceNames.Where(d => d.Name == device.NameEntity.Name).First();
				var complect = context.Complects.Where(c => c.Name == device.ComplectEntity.User && c.Cabinet.Name == device.ComplectEntity.Cabinet && c.Cabinet.Building.Name == device.ComplectEntity.BuildingEntity.Name).First();
				var deviceProvider = context.DeviceProviders.First();
				if (!context.Devices.Where(d => d.Serial == serial).Any())
				{
					context.Devices.Add(new()
					{
						Serial = serial,
						Inventory = inventory,
						DeviceName = deviceName,
						Complect = complect,
						DeviceProvider = deviceProvider
					});
					context.SaveChanges();
				}
			}
		}
	}
}
