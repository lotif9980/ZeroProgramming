using System.ComponentModel.DataAnnotations;

namespace ZPWEB.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string? Code { get; set; }
        [Required]
        public string? CourseName { get; set; }
        [Required]
        public decimal CourseFee { get; set; }
        [Required]
        public string? Duration { get; set; }
        public bool Status { get; set; } = true;

    }
}
