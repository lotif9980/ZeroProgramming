using ZPWEB.Data;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public class ScheduleRepository:IScheduleRepository
    {
        protected readonly Db _db;
        public ScheduleRepository(Db db)
        {
            _db = db;
        }

        public List<ScheduleVM> GetAll()
        {
           var data=(from sc in _db.Schedules
                     join c in _db.Courses on sc.CourseId equals c.Id
                     join i in _db.Instractors on sc.InstractorId equals i.Id 
                     select new ScheduleVM
                     {
                         Id=sc.Id,
                         Code=sc.Code,
                         CourseName=c.CourseName,
                         Day = sc.Day,
                         StartTime =sc.StartTime,
                         EndTime=sc.EndTime,
                         InstractorName=i.Name
                     }).ToList();

            return data;
        }
    }
}
