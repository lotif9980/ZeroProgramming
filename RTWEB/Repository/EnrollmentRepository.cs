using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.IdentityModel.Tokens;
using ZPWEB.Data;
using ZPWEB.Models;
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

        

        public IEnumerable<IndexEnrollment> GetAll()
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
        public IEnumerable<IndexEnrollment> GetDueAmtount()
                {
                    var data = (from e in _db.Enrollments where e.DueAmount >0
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

        public string CreateGenerateCode()
        {
            var lastEnrollmentCode=_db.Enrollments.OrderByDescending(e=>e.Id).Select(d=>d.Code).FirstOrDefault();

            string newCode = "00001";

            if(!string.IsNullOrEmpty(lastEnrollmentCode) && int.TryParse(lastEnrollmentCode,out int lastCode))
            {
                newCode=(lastCode +1).ToString("D5");
            }
            return newCode;
        }

        public void Save(Enrollment enrollment)
        {
          _db.Enrollments.Add(enrollment);
        }

        public bool DuplicateCheck(int studentId, int coursId)
        {
           return _db.Enrollments.Any(x=>x.StudentId==studentId && x.CourseId==coursId);
        }

        public void Delete(int id)
        {
           var data=  _db.Enrollments.Find(id);
            _db.Remove(data);
        }

        public Enrollment GetById(int id)
        {
           return _db.Enrollments.Find(id);
        }

        public void Update(Enrollment enrollment)
        {
            _db.Enrollments.Update(enrollment);
        }
    }
}
