namespace ZPWEB.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? InstractorId { get; set; }
    }
}
