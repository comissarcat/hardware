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

        public DbSet<Repairman> Repairmen { get; set; }
        public DbSet<RepairOperation> RepairOperations { get; set; }
        public DbSet<CompletedRepairOperation> CompletedRepairOperations { get; set; }

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
                        .HasForeignKey(d => d.ComplectId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceType>()
                        .HasMany(dt => dt.DeviceNames)
                        .WithOne(dn => dn.DeviceType)
                        .HasForeignKey(dn => dn.DeviceTypeId);

            modelBuilder.Entity<DeviceName>()
                        .HasMany(dn => dn.Devices)
                        .WithOne(d => d.DeviceName)
                        .HasForeignKey(d => d.DeviceNameId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceProvider>()
                        .HasMany(dp => dp.Devices)
                        .WithOne(d => d.DeviceProvider)
                        .HasForeignKey(d => d.DeviceProviderId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Device>()
                        .HasIndex(d => d.Serial)
                        .IsUnique();

            modelBuilder.Entity<Device>()
                        .HasMany(d => d.CompletedRepairOperations)
                        .WithOne(c => c.Device)
                        .HasForeignKey(c => c.DeviceId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Repairman>()
                        .HasMany(r => r.CompletedRepairOperations)
                        .WithOne(c => c.Repairman)
                        .HasForeignKey(c => c.RepairmanId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairOperation>()
                        .HasMany(r => r.CompletedRepairOperations)
                        .WithOne(c => c.RepairOperation)
                        .HasForeignKey(c => c.RepairOperationId)
                        .OnDelete(DeleteBehavior.Restrict);
        }

        #region Create
        public async Task<bool> CreateEntity(Type entityType, string name)
        {
            bool result = false;
            if (entityType == typeof(Building))
                result = await CreateBuilding(name);
            else if (entityType == typeof(DeviceType))
                result = await CreateDeviceType(name);
            else if (entityType == typeof(DeviceProvider))
                result = await CreateDeviceProvider(name);
            else if (entityType == typeof(Repairman))
                result = await CreateRepairman(name);
            else if (entityType == typeof(RepairOperation))
                result = await CreateRepairOperation(name);
            return result;
        }

        public async Task<bool> CreateEntity(object parent, string name)
        {
            bool result = false;

            switch (parent)
            {
                case Building:
                    result = await CreateCabinet(parent as Building, name);
                    break;
                case Cabinet:
                    result = await CreateComplect(parent as Cabinet, name);
                    break;
                case DeviceType:
                    result = await CreateDeviceName(parent as DeviceType, name);
                    break;
            }

            return result;
        }

        private async Task<bool> CreateBuilding(string name)
        {
            if (await Buildings.AnyAsync(b => b.Name.ToLower() == name.ToLower()))
                return false;

            Building newBuilding = new() { Name = name };
            await Buildings.AddAsync(newBuilding);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateCabinet(Building? building, string name)
        {
            if (building == null)
                return false;

            building = await Buildings.Include(c => c.Cabinets)
                                      .FirstOrDefaultAsync(b => b.Id == building.Id);
            if (building == null)
                return false;

            if (building.Cabinets.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            Cabinet newCabinet = new() { Building = building, Name = name };
            await Cabinets.AddAsync(newCabinet);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateComplect(Cabinet? cabinet, string name)
        {
            if (cabinet == null)
                return false;

            cabinet = await Cabinets.Include(c => c.Complects)
                                      .FirstOrDefaultAsync(b => b.Id == cabinet.Id);
            if (cabinet == null)
                return false;

            if (cabinet.Complects.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            Complect newComplect = new() { Cabinet = cabinet, Name = name };
            await Complects.AddAsync(newComplect);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateDeviceType(string name)
        {
            if (await DeviceTypes.AnyAsync(b => b.Name.ToLower() == name.ToLower()))
                return false;

            DeviceType newDeviceType = new() { Name = name };
            await DeviceTypes.AddAsync(newDeviceType);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateDeviceName(DeviceType? deviceType, string name)
        {
            if (deviceType == null)
                return false;

            deviceType = await DeviceTypes.Include(c => c.DeviceNames)
                                      .FirstOrDefaultAsync(b => b.Id == deviceType.Id);
            if (deviceType == null)
                return false;

            if (deviceType.DeviceNames.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            DeviceName newDeviceName = new() { DeviceType = deviceType, Name = name };
            await DeviceNames.AddAsync(newDeviceName);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateDeviceProvider(string name)
        {
            if (await DeviceProviders.AnyAsync(b => b.Name.ToLower() == name.ToLower()))
                return false;

            DeviceProvider newDeviceProvider = new() { Name = name };
            await DeviceProviders.AddAsync(newDeviceProvider);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateDevice(Complect? complect, DeviceProvider? deviceProvider, DeviceName? deviceName, string serial, string? inventory, string? notes)
        {
            if (complect == null || deviceProvider == null || deviceName == null || serial == string.Empty || Devices.Any(d => d.Serial.ToLower() == serial.ToLower()))
                return false;

            complect = await Complects.Include(c => c.Cabinet)
                                      .ThenInclude(c => c.Building)
                                      .FirstOrDefaultAsync(c => c.Id == complect.Id);
            deviceProvider = await DeviceProviders.FindAsync(deviceProvider.Id);
            deviceName = await DeviceNames.FindAsync(deviceName.Id);

            if (complect == null || deviceProvider == null || deviceName == null)
                return false;

            Device newDevice = new()
            {
                Complect = complect,
                DeviceProvider = deviceProvider,
                DeviceName = deviceName,
                Serial = serial,
                Inventory = inventory,
                Notes = notes
            };

            string before = "АБСОЛЮТНО НИЧЕГО";
            string after = newDevice.ToStringForHistory();
            await History.AddAsync(new History() { Before = before, After = after });
            await Devices.AddAsync(newDevice);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateRepairman(string name)
        {
            if (await Repairmen.AnyAsync(r => r.Name.ToLower() == name.ToLower()))
                return false;

            Repairman newRepairman = new() { Name = name };
            await Repairmen.AddAsync(newRepairman);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> CreateRepairOperation(string name)
        {
            if (await RepairOperations.AnyAsync(r => r.Name.ToLower() == name.ToLower()))
                return false;

            RepairOperation newRepairOperation = new() { Name = name };
            await RepairOperations.AddAsync(newRepairOperation);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateCompletedRepairOperation(Device? device, Repairman? repairman, RepairOperation? repairOperation, DateOnly? date, string? notes)
        {
            if (device == null || repairman == null || repairOperation == null || date == null)
                return false;

            device = await Devices.FindAsync(device.Id);
            repairman = await Repairmen.FindAsync(repairman.Id);
            repairOperation = await RepairOperations.FindAsync(repairOperation.Id);

            if (device == null || repairman == null || repairOperation == null || date == null)
                return false;

            CompletedRepairOperation newCompletedRepairOperation = new()
            {
                Device = device,
                RepairOperation = repairOperation,
                Repairman = repairman,
                Date = date.Value,
                Notes = notes
            };

            await CompletedRepairOperations.AddAsync(newCompletedRepairOperation);
            await SaveChangesAsync();
            return true;
        }
        #endregion

        #region Read
        public async Task<List<Building>> ReadBuildings(string filter)
        {
            List<Building> buildings = await Buildings.Include(b => b.Cabinets)
                                                      .ThenInclude(c => c.Complects)
                                                      .ThenInclude(c => c.Devices)
                                                      .ThenInclude(d => d.DeviceName)
                                                      .OrderBy(b => b.Name)
                                                      .AsSplitQuery()
                                                      .AsNoTracking()
                                                      .ToListAsync();
            if (filter.Length != 0)
            {
                buildings = [.. buildings.Where(b => b.Cabinets.Any(c => c.Complects.Any(c => c.Devices.Any(d => d.ToString()
                                                                                                                  .ToLower()
                                                                                                                  .Contains(filter.ToLower())))))];
                foreach (Building building in buildings)
                {
                    building.Cabinets = [.. building.Cabinets.Where(c => c.Complects.Any(c => c.Devices.Any(d => d.ToString()
                                                                                                                  .ToLower()
                                                                                                                  .Contains(filter.ToLower()))))];
                    foreach (Cabinet cabinet in building.Cabinets)
                    {
                        cabinet.Complects = [.. cabinet.Complects.Where(c => c.Devices.Any(d => d.ToString()
                                                                                                 .ToLower()
                                                                                                 .Contains(filter.ToLower())))];

                        foreach (Complect complect in cabinet.Complects)
                            complect.Devices = [.. complect.Devices.Where(d => d.ToString()
                                                                                .ToLower()
                                                                                .Contains(filter.ToLower()))];
                    }
                }
            }
            return buildings;
        }

        public async Task<List<DeviceType>> ReadDeviceTypes(string filter)
        {
            List<DeviceType> deviceTypes = await DeviceTypes.Include(dt => dt.DeviceNames)
                                                            .ThenInclude(dn => dn.Devices)
                                                            .ThenInclude(d => d.Complect)
                                                            .ThenInclude(c => c.Cabinet)
                                                            .ThenInclude(c => c.Building)
                                                            .OrderBy(dt => dt.Name)
                                                            .AsSplitQuery()
                                                            .AsNoTracking()
                                                            .ToListAsync();
            if (filter.Length != 0)
            {
                deviceTypes = [.. deviceTypes.Where(dt => dt.DeviceNames.Any(dn => dn.Devices.Any(d => d.ToString()
                                                                                                        .ToLower()
                                                                                                        .Contains(filter.ToLower()))))];
                foreach (DeviceType deviceType in deviceTypes)
                {
                    deviceType.DeviceNames = [.. deviceType.DeviceNames.Where(dn => dn.Devices.Any(d => d.ToString()
                                                                                                         .ToLower()
                                                                                                         .Contains(filter.ToLower())))];
                    foreach (DeviceName deviceName in deviceType.DeviceNames)
                        deviceName.Devices = [.. deviceName.Devices.Where(d => d.ToString()
                                                                                .ToLower()
                                                                                .Contains(filter.ToLower()))];
                }
            }
            return deviceTypes;
        }

        public async Task<List<DeviceName>> ReadDeviceNames()
        {
            return await DeviceNames.Include(dn => dn.DeviceType)
                                    .OrderBy(dn => dn.DeviceType.Name)
                                    .ThenBy(dn => dn.Name)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<List<DeviceProvider>> ReadDeviceProviders(string filter)
        {
            List<DeviceProvider> deviceProviders = await DeviceProviders.Include(dn => dn.Devices)
                                                                        .ThenInclude(d => d.Complect)
                                                                        .ThenInclude(c => c.Cabinet)
                                                                        .ThenInclude(c => c.Building)
                                                                        .Include(dn => dn.Devices)
                                                                        .ThenInclude(d => d.DeviceName)
                                                                        .OrderBy(dt => dt.Name)
                                                                        .AsSplitQuery()
                                                                        .AsNoTracking()
                                                                        .ToListAsync();
            if (filter.Length != 0)
            {
                deviceProviders = [.. deviceProviders.Where(dt => dt.Devices.Any(d => d.ToString()
                                                                                       .ToLower()
                                                                                       .Contains(filter.ToLower())))];
                foreach (DeviceProvider deviceProvider in deviceProviders)
                    deviceProvider.Devices = [.. deviceProvider.Devices.Where(d => d.ToString()
                                                                                    .ToLower()
                                                                                    .Contains(filter.ToLower()))];
            }
            return deviceProviders;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateEntity(object entity, string newName)
        {
            bool result = false;

            switch (entity)
            {
                case Building item:
                    result = await UpdateBuilding(item, newName);
                    break;
                case Cabinet item:
                    result = await UpdateCabinet(item, newName);
                    break;
                case Complect item:
                    result = await UpdateComplect(item, newName);
                    break;
                case DeviceType item:
                    result = await UpdateDeviceType(item, newName);
                    break;
                case DeviceName item:
                    result = await UpdateDeviceName(item, newName);
                    break;
                case DeviceProvider item:
                    result = await UpdateDeviceProvider(item, newName);
                    break;
                case Repairman item:
                    result = await UpdateRepairman(item, newName);
                    break;
                case RepairOperation item:
                    result = await UpdateRepairOperation(item, newName);
                    break;
            }

            return result;
        }

        public async Task<bool> UpdateEntity(object entity, object newParent)
        {
            bool result = false;

            switch (entity, newParent)
            {
                case (Cabinet item, Building parent):
                    result = await UpdateCabinet(item, parent);
                    break;
                case (Complect item, Cabinet parent):
                    result = await UpdateComplect(item, parent);
                    break;
                case (Device item, Complect parent):
                    result = await UpdateDevice(item, parent);
                    break;
                case (DeviceName item, DeviceType parent):
                    result = await UpdateDeviceName(item, parent);
                    break;
                case (Device item, DeviceName parent):
                    result = await UpdateDevice(item, parent);
                    break;
                case (Device item, DeviceProvider parent):
                    result = await UpdateDevice(item, parent);
                    break;
            }

            return result;
        }

        private async Task<bool> UpdateBuilding(Building? building, string newName)
        {
            if (building == null)
                return false;

            if (await Buildings.AnyAsync(b => b.Name.ToLower() == newName.ToLower()))
                return false;

            building = await Buildings.FindAsync(building.Id);
            if (building == null)
                return false;

            building.Name = newName;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateCabinet(Cabinet? cabinet, string name)
        {
            if (cabinet == null)
                return false;

            cabinet = await Cabinets.Include(c => c.Building)
                                    .Include(c => c.Complects)
                                    .ThenInclude(c => c.Devices)
                                    .ThenInclude(d => d.DeviceName)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(c => c.Id == cabinet.Id);
            if (cabinet == null)
                return false;

            if (cabinet.Building.Cabinets.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            List<Device> devicesInCabinet = [.. cabinet.Complects.SelectMany(c => c.Devices)];

            List<string> before = [];
            foreach (Device device in devicesInCabinet)
                before.Add(device.ToStringForHistory());

            cabinet.Name = name;

            List<string> after = [];
            foreach (Device device in devicesInCabinet)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInCabinet.Count; i++)
            {
                History history = new() { Before = before[i], After = after[i] };
                await History.AddAsync(history);
            }

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateCabinet(Cabinet? cabinet, Building? building)
        {
            if (cabinet == null || building == null)
                return false;

            cabinet = await Cabinets.Include(c => c.Building)
                                    .Include(c => c.Complects)
                                    .ThenInclude(c => c.Devices)
                                    .ThenInclude(d => d.DeviceName)
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(c => c.Id == cabinet.Id);
            building = await Buildings.FindAsync(building.Id);

            if (cabinet == null || building == null)
                return false;

            if (building.Cabinets.Any(c => c.Name.ToLower() == cabinet.Name.ToLower()))
                return false;

            List<Device> devicesInCabinet = [.. cabinet.Complects.SelectMany(c => c.Devices)];

            List<string> before = [];
            foreach (Device device in devicesInCabinet)
                before.Add(device.ToStringForHistory());

            cabinet.Building = building;

            List<string> after = [];
            foreach (Device device in devicesInCabinet)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInCabinet.Count; i++)
            {
                History history = new() { Before = before[i], After = after[i] };
                await History.AddAsync(history);
            }

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateComplect(Complect? complect, string name)
        {
            if (complect == null)
                return false;

            complect = await Complects.Include(c => c.Cabinet)
                                      .ThenInclude(c => c.Building)
                                      .Include(c => c.Devices)
                                      .ThenInclude(d => d.DeviceName)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(c => c.Id == complect.Id);
            if (complect == null)
                return false;

            if (complect.Cabinet.Complects.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            List<Device> devicesInComplect = [.. complect.Devices];

            List<string> before = [];
            foreach (Device device in devicesInComplect)
                before.Add(device.ToStringForHistory());

            complect.Name = name;

            List<string> after = [];
            foreach (Device device in devicesInComplect)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInComplect.Count; i++)
            {
                History history = new() { Before = before[i], After = after[i] };
                await History.AddAsync(history);
            }

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateComplect(Complect? complect, Cabinet? cabinet)
        {
            if (complect == null || cabinet == null)
                return false;

            complect = await Complects.Include(c => c.Cabinet)
                                      .ThenInclude(c => c.Building)
                                      .Include(c => c.Devices)
                                      .ThenInclude(d => d.DeviceName)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(c => c.Id == complect.Id);
            cabinet = await Cabinets.Include(c => c.Building)
                                    .FirstOrDefaultAsync(c => c.Id == cabinet.Id);

            if (complect == null || cabinet == null)
                return false;

            if (cabinet.Complects.Any(c => c.Name.ToLower() == complect.Name.ToLower()))
                return false;

            List<Device> devicesInComplect = [.. complect.Devices];

            List<string> before = [];
            foreach (Device device in devicesInComplect)
                before.Add(device.ToStringForHistory());

            complect.Cabinet = cabinet;

            List<string> after = [];
            foreach (Device device in devicesInComplect)
                after.Add(device.ToStringForHistory());

            for (int i = 0; i < devicesInComplect.Count; i++)
            {
                History history = new() { Before = before[i], After = after[i] };
                await History.AddAsync(history);
            }

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDeviceType(DeviceType? deviceType, string newName)
        {
            if (deviceType == null)
                return false;

            if (await DeviceTypes.AnyAsync(b => b.Name.ToLower() == newName.ToLower()))
                return false;

            deviceType = await DeviceTypes.FindAsync(deviceType.Id);
            if (deviceType == null)
                return false;

            deviceType.Name = newName;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDeviceName(DeviceName? deviceName, string name)
        {
            if (deviceName == null)
                return false;

            deviceName = await DeviceNames.Include(dn => dn.DeviceType)
                                          .FirstOrDefaultAsync(c => c.Id == deviceName.Id);
            if (deviceName == null)
                return false;

            if (deviceName.DeviceType.DeviceNames.Any(c => c.Name.ToLower() == name.ToLower()))
                return false;

            deviceName.Name = name;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDeviceName(DeviceName? deviceName, DeviceType? deviceType)
        {
            if (deviceName == null || deviceType == null)
                return false;

            deviceName = await DeviceNames.FindAsync(deviceName.Id);
            deviceType = await DeviceTypes.FindAsync(deviceType.Id);

            if (deviceName == null || deviceType == null)
                return false;

            if (deviceType.DeviceNames.Any(c => c.Name.ToLower() == deviceName.Name.ToLower()))
                return false;

            deviceName.DeviceType = deviceType;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDeviceProvider(DeviceProvider? deviceProvider, string newName)
        {
            if (deviceProvider == null)
                return false;

            if (await DeviceProviders.AnyAsync(b => b.Name.ToLower() == newName.ToLower()))
                return false;

            deviceProvider = await DeviceProviders.FindAsync(deviceProvider.Id);
            if (deviceProvider == null)
                return false;

            deviceProvider.Name = newName;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDevice(Device? device, Complect? complect)
        {
            if (device == null || complect == null)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);
            complect = await Complects.Include(c => c.Cabinet)
                                      .ThenInclude(c => c.Building)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(c => c.Id == complect.Id);

            if (device == null || complect == null)
                return false;

            string before = device.ToStringForHistory();

            device.Complect = complect;

            string after = device.ToStringForHistory();

            History history = new() { Before = before, After = after };
            await History.AddAsync(history);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDevice(Device? device, string serial, string? inventory)
        {
            if (device == null || serial == string.Empty)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);

            if (device == null)
                return false;

            string before = device.ToStringForHistory();

            device.Serial = serial;
            device.Inventory = inventory;

            string after = device.ToStringForHistory();

            History history = new() { Before = before, After = after };
            await History.AddAsync(history);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDevice(Device? device, string? notes)
        {
            if (device == null)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);

            if (device == null)
                return false;

            device.Notes = notes;

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDevice(Device? device, DeviceProvider? deviceProvider)
        {
            if (device == null || deviceProvider == null)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);
            deviceProvider = await DeviceProviders.FindAsync(deviceProvider.Id);

            if (device == null || deviceProvider == null)
                return false;

            device.DeviceProvider = deviceProvider;

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateDevice(Device? device, DeviceName? deviceName)
        {
            if (device == null || deviceName == null)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);
            deviceName = await DeviceNames.FindAsync(deviceName.Id);

            if (device == null || deviceName == null)
                return false;

            device.DeviceName = deviceName;

            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateRepairman(Repairman? repairman, string newName)
        {
            if (repairman == null)
                return false;

            if (await Repairmen.AnyAsync(b => b.Name.ToLower() == newName.ToLower()))
                return false;

            repairman = await Repairmen.FindAsync(repairman.Id);
            if (repairman == null)
                return false;

            repairman.Name = newName;
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> UpdateRepairOperation(RepairOperation? repairOperation, string newName)
        {
            if (repairOperation == null)
                return false;

            if (await RepairOperations.AnyAsync(b => b.Name.ToLower() == newName.ToLower()))
                return false;

            repairOperation = await RepairOperations.FindAsync(repairOperation.Id);
            if (repairOperation == null)
                return false;

            repairOperation.Name = newName;
            await SaveChangesAsync();
            return true;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteEntity(object entity)
        {
            bool result = false;

            switch (entity)
            {
                case Building:
                    result = await DeleteBuilding(entity as Building);
                    break;
                case Cabinet:
                    result = await DeleteCabinet(entity as Cabinet);
                    break;
                case Complect:
                    result = await DeleteComplect(entity as Complect);
                    break;
                case DeviceType:
                    result = await DeleteDeviceType(entity as DeviceType);
                    break;
                case DeviceName:
                    result = await DeleteDeviceName(entity as DeviceName);
                    break;
                case DeviceProvider:
                    result = await DeleteDeviceProvider(entity as DeviceProvider);
                    break;
                case Device:
                    result = await DeleteDevice(entity as Device);
                    break;
                case Repairman:
                    result = await DeleteRepairman(entity as Repairman); ;
                    break;
                case RepairOperation:
                    result = await DeleteRepairOperation(entity as RepairOperation);
                    break;
            }

            return result;
        }

        private async Task<bool> DeleteBuilding(Building? building)
        {
            if (building == null)
                return false;

            building = await Buildings.Include(b => b.Cabinets)
                                      .ThenInclude(c => c.Complects)
                                      .ThenInclude(c => c.Devices)
                                      .AsSplitQuery()
                                      .FirstOrDefaultAsync(b => b.Id == building.Id);
            if (building == null)
                return false;

            if (building.Cabinets.Any(c => c.Complects.Any(c => c.Devices.Count != 0)))
                return false;

            Buildings.Remove(building);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteCabinet(Cabinet? cabinet)
        {
            if (cabinet == null)
                return false;

            cabinet = await Cabinets.Include(c => c.Complects)
                                    .ThenInclude(c => c.Devices)
                                    .FirstOrDefaultAsync(c => c.Id == cabinet.Id);

            if (cabinet == null)
                return false;

            if (cabinet.Complects.Any(c => c.Devices.Count != 0))
                return false;

            Cabinets.Remove(cabinet);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteComplect(Complect? complect)
        {
            if (complect == null)
                return false;

            complect = await Complects.Include(c => c.Devices)
                                      .FirstOrDefaultAsync(c => c.Id == complect.Id);

            if (complect == null)
                return false;

            if (complect.Devices.Count != 0)
                return false;

            Complects.Remove(complect);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteDeviceProvider(DeviceProvider? deviceProvider)
        {
            if (deviceProvider == null)
                return false;

            deviceProvider = await DeviceProviders.FindAsync(deviceProvider.Id);
            if (deviceProvider == null)
                return false;

            if (deviceProvider.Devices.Count != 0)
                return false;

            DeviceProviders.Remove(deviceProvider);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteDeviceType(DeviceType? deviceType)
        {
            if (deviceType == null)
                return false;

            deviceType = await DeviceTypes.Include(dt => dt.DeviceNames)
                                          .ThenInclude(dn => dn.Devices)
                                          .AsSplitQuery()
                                          .FirstOrDefaultAsync(dt => dt.Id == deviceType.Id);
            if (deviceType == null)
                return false;

            if (deviceType.DeviceNames.Any(dn => dn.Devices.Count != 0))
                return false;

            DeviceTypes.Remove(deviceType);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteDeviceName(DeviceName? deviceName)
        {
            if (deviceName == null)
                return false;

            deviceName = await DeviceNames.Include(c => c.Devices)
                                          .FirstOrDefaultAsync(c => c.Id == deviceName.Id);

            if (deviceName == null)
                return false;

            if (deviceName.Devices.Count != 0)
                return false;

            DeviceNames.Remove(deviceName);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteDevice(Device? device)
        {
            if (device == null)
                return false;

            device = await Devices.Include(d => d.DeviceName)
                                  .Include(d => d.Complect)
                                  .ThenInclude(c => c.Cabinet)
                                  .ThenInclude(c => c.Building)
                                  .AsSplitQuery()
                                  .FirstOrDefaultAsync(d => d.Id == device.Id);

            if (device == null)
                return false;

            string before = device.ToStringForHistory();
            string after = "УДАЛЕНО";
            Devices.Remove(device);
            await History.AddAsync(new History() { Before = before, After = after });
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCompletedRepairOperation(int id)
        {
            CompletedRepairOperation? operation = await CompletedRepairOperations.FindAsync(id);
            if (operation == null)
                return false;

            CompletedRepairOperations.Remove(operation);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteRepairman(Repairman? repairman)
        {
            if (repairman == null)
                return false;

            repairman = await Repairmen.Include(r => r.CompletedRepairOperations)
                                       .FirstOrDefaultAsync(r => r.Id == repairman.Id);
            if (repairman == null)
                return false;

            if (repairman.CompletedRepairOperations.Count != 0)
                return false;

            Repairmen.Remove(repairman);
            await SaveChangesAsync();
            return true;
        }

        private async Task<bool> DeleteRepairOperation(RepairOperation? repairOperation)
        {
            if (repairOperation == null)
                return false;

            repairOperation = await RepairOperations.Include(r => r.CompletedRepairOperations)
                                                    .FirstOrDefaultAsync(r => r.Id == repairOperation.Id);
            if (repairOperation == null)
                return false;

            if (repairOperation.CompletedRepairOperations.Count != 0)
                return false;

            RepairOperations.Remove(repairOperation);
            await SaveChangesAsync();
            return true;
        }
        #endregion                
    }
}
