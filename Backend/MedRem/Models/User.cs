using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedRem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string? Token{ get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IllnessHistory { get; set; }

        // Navigation property for the pills schedule
        public virtual ICollection<PillSchedule>? PillSchedules { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
        public virtual ICollection<Pill>? Pills { get; set; }

    }
}