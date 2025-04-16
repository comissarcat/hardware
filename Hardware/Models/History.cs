namespace Hardware.Models
{
	public class History
	{
		public int Id { get; set; }
		public string Before { get; set; }
		public string After { get; set; }
		public DateTime ChangedAt { get; set; }

		public History()
		{
			ChangedAt = DateTime.Now;
		}
	}
}
