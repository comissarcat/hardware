namespace Hardware.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string? Inventory { get; set; }

        public int DeviceNameId { get; set; }
        public DeviceName DeviceName { get; set; }

        public int ComplectId { get; set; }
        public Complect Complect { get; set; }

        public int DeviceProviderId { get; set; }
        public DeviceProvider DeviceProvider { get; set; }

        public string? Notes { get; set; }

        public ICollection<CompletedRepairOperation> CompletedRepairOperations { get; set; } = [];

        public override string ToString()
        {
            return $"{DeviceName.Name} {Serial} {Inventory}";
        }

        public string ToFullString()
        {
            return $"{Complect.Cabinet.Building.Name} {Complect.Cabinet.Name} {Complect.Name} {DeviceName.DeviceType.Name} {DeviceName.Name} {DeviceProvider.Name} {Serial} {Inventory} {Notes}";
        }

        public string ToStringForHistory()
        {
            return $"Здание: {Complect.Cabinet.Building.Name}; кабинет: {Complect.Cabinet.Name}; комплект: {Complect.Name}; название: {DeviceName}; с/н: {Serial}; и/н: {Inventory}; примечание: {Notes}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Device)
                return Id == (obj as Device).Id;
            return false;
        }
    }
}
