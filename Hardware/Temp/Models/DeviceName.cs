using System.ComponentModel.DataAnnotations.Schema;

namespace Hardware.Temp.Models
{
	internal class DeviceName
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Type {  get; set; }
		[ForeignKey("type")]
		public DeviceType TypeEntity { get; set; }
	}
}
