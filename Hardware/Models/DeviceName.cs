namespace Hardware.Models
{
    public class DeviceName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        public ICollection<Device> Devices { get; set; } = [];

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is DeviceName)
                return Id == (obj as DeviceName).Id;
            return false;
        }
    }
}
