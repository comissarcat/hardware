using System.ComponentModel.DataAnnotations.Schema;

namespace Hardware.Temp.Models
{
	internal class Device
	{
		public int Id { get; set; }
		public int Name { get; set; }
		[ForeignKey("name")]
		public DeviceName NameEntity { get; set; }
		public string Serial_number { get; set; }
		public string Inventory_number { get; set; }
		public int Complect { get; set; }
		[ForeignKey("complect")]
		public Complect ComplectEntity { get; set; }
		public string Notes { get; set; }
	}
}
