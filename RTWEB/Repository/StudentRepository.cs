using ZPWEB.Data;
using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public class StudentRepository :IStudentRepository
    {
        protected readonly Db _db;
        public StudentRepository(Db db)
        {
            _db = db;
        }

        public List<Student> GetAll()
        {
          return _db.Students.ToList();
        }
    }
}
