using ZPWEB.Data;
using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public class CourseRepository : ICourseRepository
    {
        protected readonly Db _db;
        public CourseRepository(Db db)
        {
           _db= db;
        }

        public IEnumerable<Course> GetAll()
        {
            return _db.Courses.ToList();
        }
        public IEnumerable<Course> ActiveGetAll()
        {
            return _db.Courses.Where(x=>x.Status==true).ToList();
        }

        public string GenerateCode()
        {
           var lastCourseCode=_db.Courses.OrderByDescending(c=>c.Id).Select(c=>c.Code).FirstOrDefault();

            string newCode = "00001";
            if(!string.IsNullOrEmpty(lastCourseCode) && int.TryParse(lastCourseCode, out int lastcode))
            {
                newCode = (lastcode + 1).ToString("D5");
            }

            return newCode;
        }

        public void Save(Course course)
        {
           _db.Courses.Add(course);
        }

        public bool ExestingCheck(string name)
        {
           return _db.Courses.Any(x=>x.CourseName==name);
        }

        public void Delete(int id)
        {
            var data = _db.Courses.Find(id);
            _db.Remove(data);
        }

        public Course GetById(int id)
        {
            return _db.Courses.Find(id);
        }

        public bool TransactionCheck(int id)
        {
          return _db.Enrollments.Any(x=>x.CourseId==id);
        }

        public void UpdateStatus(int id)
        {
            var course = _db.Courses.Find(id);
            course.Status = !course.Status;
        }

        public void Update(Course course)
        {
            _db.Update(course);
        }
    }
}
