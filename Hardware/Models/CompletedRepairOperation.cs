namespace Hardware.Models
{
    public class CompletedRepairOperation
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int RepairOperationId { get; set; }
        public RepairOperation RepairOperation { get; set; }
        public int RepairmanId { get; set; }
        public Repairman Repairman { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
    }
}
