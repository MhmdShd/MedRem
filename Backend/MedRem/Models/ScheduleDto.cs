using System.ComponentModel.DataAnnotations;

namespace MedRem.Models
{
    public class ScheduleDto
    {
        public int? PillId { get; set; } // Nullable if the client might not send a pill ID
        public int UserId { get; set; }
        public string? PillName { get; set; } // name
        public string? PillShape { get; set; } // Used if PillId is not provided
        public string? PillType { get; set; } // Category
        public string? PillColor { get; set; } // color
        public string Description { get; set; } //description
        public string? Image { get; set; } // image
        public string FinishDate { get; set; } //stopdate
        public string Dosage { get; set; } // E.g., "100mg", "2 tablets", etc.

        public List<string> Frequency { get; set; } //SelectedDays
        public List<string> Time { get; set; } //time
    }
}