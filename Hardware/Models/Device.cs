namespace Hardware.Models
{
	public class Device
	{
		public int Id { get; set; }
		public string Serial { get; set; }
		public string? Inventory { get; set; }
		public DeviceName DeviceName { get; set; }
		public Complect Complect { get; set; }
		public DeviceProvider DeviceProvider { get; set; }
		public override string ToString()
		{
			return $"{DeviceName.Name} {Serial} {Inventory}";
		}
	}
}
