using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public TimeOnly Time { get; set; }

        public string TimeAsString
        {
            get => Time.ToString("HH:mm");
            set => Time = TimeOnly.Parse(value);
        }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Pill? Pill { get; set; }
    }
}