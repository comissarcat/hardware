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

		public override bool Equals(object? obj)
		{
			if (obj is DeviceProvider)
				return Id == (obj as DeviceProvider).Id;
			return false;
		}
	}
}
