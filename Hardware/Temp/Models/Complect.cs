using System.ComponentModel.DataAnnotations.Schema;

namespace Hardware.Temp.Models
{
	internal class Complect
	{
		public int Id { get; set; }
		public int Building { get; set; }
		[ForeignKey("building")]
		public Building BuildingEntity { get; set; }
		public string Cabinet { get; set; }
		public string User { get; set; }
	}
}
