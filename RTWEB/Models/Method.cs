using System.ComponentModel.DataAnnotations;

namespace ZPWEB.Models
{
    public class Method
    {
        public int Id { get; set; }
        public string? Code {  get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
