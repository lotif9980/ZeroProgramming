using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZPWEB.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Code {  get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ContactNo { get; set; }
        [Required]
        public string? Address { get; set; }

        public string? FatherName { get; set; }
        public string? FatherPhoneNumber { get; set; }
        public string? MotherName { get; set; }
        public string? SchoolName { get; set; }
        public string? Picture { get; set; }
        public string? Class { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
