namespace Hardware.Models
{
	public class DeviceType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object? obj)
		{
			if (obj is DeviceType)
				return Id == (obj as DeviceType).Id;
			return false;
		}
	}
}
