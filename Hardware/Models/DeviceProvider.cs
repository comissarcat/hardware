namespace Hardware.Models
{
	public class DeviceProvider
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
