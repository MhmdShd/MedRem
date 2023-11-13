using System.ComponentModel.DataAnnotations;

namespace MedRem.Models
{
    public class Report
    {

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public string? Image { get; set; }
        public string InsidanceDate { get; set; }
        public string ReportDate { get; set; }

        public virtual User? User { get; set; }
    }
}