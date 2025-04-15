namespace Hardware.Models
{
	public class Complect
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Cabinet Cabinet { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
