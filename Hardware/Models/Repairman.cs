namespace Hardware.Models
{
    public class Repairman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CompletedRepairOperation> CompletedRepairOperations { get; set; } = [];

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Repairman repairman)
                return Id == repairman.Id;
            return false;
        }
    }
}
