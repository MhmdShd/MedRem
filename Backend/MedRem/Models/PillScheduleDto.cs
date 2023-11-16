using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedRem.Models
{
    public class PillScheduleDto
    {
        public int Id { get; set; }
        // ... other properties ...
        public int UserId { get; set; }
        public int PillId { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; } // E.g., "Every 8 hours", "Once a day", etc.
        [NotMapped]
        public TimeOnly Time { get; set; }

        public string TimeAsString
        {
            get => Time.ToString("HH:mm");
            set => Time = TimeOnly.Parse(value);
        }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public PillDto Pill { get; set; }
    }

    public class PillDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Type { get; set; } // E.g., pill, syrup, capsule, etc.
        public string Description { get; set; }
        public string? Image { get; set; }
        // Navigation property for the pill schedule
        // ... other properties but no navigation properties ...
    }

}