using Microsoft.AspNetCore.Http.Metadata;
using ZPWEB.Data;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        protected readonly Db _db;
        public EnrollmentRepository(Db db)
        {
            _db = db;
        }

        public List<IndexEnrollment> GetAll()
        {
            var data = (from e in _db.Enrollments
                        join c in _db.Courses on e.CourseId equals c.Id
                        join s in _db.Students on e.StudentId equals s.Id
                        join sc in _db.Schedules on e.ScheduleId equals sc.Id
                        select new IndexEnrollment 
                        { 
                         Id= e.Id,
                         Code=e.Code,
                         StudentName=s.Name,
                         CourseName=c.CourseName,
                         EnrollDate=e.EnrollDate,
                         ScheduleName=sc.Name,
                         ContactNo=s.ContactNo
                        }).ToList();
            return data;
        }
    }
}
