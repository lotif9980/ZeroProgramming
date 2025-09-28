namespace ZPWEB.ViewModel
{
    public class ScheduleVM
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public int? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int? Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? InstractorId { get; set; }
        public string InstractorName { get; set; }
        public string DayName { get; set; }
    }
}
