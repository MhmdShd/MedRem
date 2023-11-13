using System.ComponentModel.DataAnnotations;

namespace MedRem.Models
{
    public class Pill
    {

        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Type { get; set; } // E.g., pill, syrup, capsule, etc.
        public string Description { get; set; }
       public string? Image { get;set; }
        // Navigation property for the pill schedule
        public virtual User? User { get; set; }  
        public virtual ICollection<PillSchedule>? PillSchedules { get; set; }

    }
}