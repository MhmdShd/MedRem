using System.ComponentModel.DataAnnotations;

namespace MedRem.Models
{
    public class PillSchedule
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PillId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; } // E.g., "Every 8 hours", "Once a day", etc.
        public string Time { get; set; } // The time of day the pill should be taken
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Pill? Pill { get; set; }
    }
}