namespace ZPWEB.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime? EnrollDate {  get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
    }
}
