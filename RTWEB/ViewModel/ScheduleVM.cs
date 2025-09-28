using ZPWEB.Models;

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

    public class ScheduleSaveVM
    {

        public Schedule Input { get; set; } = new Schedule();
        public List<Schedule> Schedule { get; set; } = new List<Schedule>();
        //public Schedule Input { get; set; } = new Schedule();
        //public Schedule? Schedule { get; set; } = new Schedule();
        public IEnumerable<Instractor> Instractors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
