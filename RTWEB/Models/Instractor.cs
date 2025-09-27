using System.ComponentModel.DataAnnotations;

namespace ZPWEB.Models
{
    public class Instractor
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? ContactNo {  get; set; }
    }
}
