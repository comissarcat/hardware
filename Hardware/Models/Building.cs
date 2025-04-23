namespace Hardware.Models
{
	public class Building
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Building)
				return Id == (obj as Building).Id;
			return false;
		}
	}
}
