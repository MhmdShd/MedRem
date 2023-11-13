using System.ComponentModel.DataAnnotations;

namespace MedRem.Models
{
    public class ScheduleDto
    {
        public int? PillId { get; set; } // Nullable if the client might not send a pill ID
        public int UserId { get; set; }
        public string? PillName { get; set; } // Used if PillId is not provided
        public string? PillShape { get; set; } // Used if PillId is not provided
        public string? PillType { get; set; } // Used if PillId is not provided
        public string? PillColor { get; set; } // Used if PillId is not provided
        public string? Image { get; set; } // Used if PillId is not provided
        public string FinishDate { get; set; }
        public string Dosage { get; set; } // E.g., "100mg", "2 tablets", etc.

        public string Frequency { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
    }
}