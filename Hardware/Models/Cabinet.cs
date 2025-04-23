namespace Hardware.Models
{
	public class Cabinet
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Building Building { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Cabinet)
				return Id == (obj as Cabinet).Id;
			return false;
		}
	}
}
