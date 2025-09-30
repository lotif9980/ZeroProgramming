using ZPWEB.Models;

namespace ZPWEB.ViewModel
{
    public class EnrollmentVM
    {
        public Enrollment Enrollment { get; set; }
        public IEnumerable<Course> Course { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }

    public class IndexEnrollment
    {
        public string? Code {  get; set; }
        public int Id { get; set; }
        public DateTime? EnrollDate { get; set; }
        public string? StudentName{ get; set; }
        public string? PhoneNumber {  get; set; }
        public string? CourseName { get; set; }
        public string? ScheduleName { get; set; }
        public string ? ContactNo { get; set; }
    }
}
