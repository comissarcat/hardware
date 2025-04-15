namespace Hardware.Models
{
	public class DeviceName
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DeviceType DeviceType { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
