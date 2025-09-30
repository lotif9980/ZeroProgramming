namespace ZPWEB.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string? Code {  get; set; }
        public DateTime? EnrollDate {  get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? ScheduleId {  get; set; }
    }
}
