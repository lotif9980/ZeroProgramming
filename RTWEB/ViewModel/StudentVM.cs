using ZPWEB.Models;

namespace ZPWEB.ViewModel
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
        public IEnumerable<Method> Methods { get; set; }
        public int? SelectCourceId { get; set; }
        public int? ScheduleId { get; set; }
        public int? MethodId { get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
    }
}
