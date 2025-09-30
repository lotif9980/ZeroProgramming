using System.Numerics;
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

        public string GenerateCode()
        {
            var lastStudentCode=_db.Students.OrderByDescending(x=>x.Id).Select(x=>x.Code).FirstOrDefault();
            string newCode = "00001";

            if(!string.IsNullOrEmpty(lastStudentCode) && int.TryParse(lastStudentCode,out int lastCode))
            {
                newCode=(lastCode +1).ToString("D5");
            }
            return newCode;
        }

        public void Save(Student student)
        {
          _db.Students.Add(student);
        }

        public bool DuplicateCheck(string name, string pNumber)
        {
           return _db.Students.Any(x=>x.Name==name && x.ContactNo==pNumber);
        }

        public void Delete(int id)
        {
            var data = _db.Students.Find(id);

            if (!string.IsNullOrEmpty(data.Picture))
            {
                string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string imagePath = Path.Combine(wwwRootPath, "Students", data.Picture);

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            _db.Students.Remove(data);
        }

        public void Update(Student student)
        {
            var exestingStudent=_db.Students.FirstOrDefault(d=>d.Id==student.Id);
            if (exestingStudent != null)
            {
                _db.Entry(exestingStudent).CurrentValues.SetValues(student);
            }
        }
    }
}
