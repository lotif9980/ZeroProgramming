using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface IStudentRepository
    {
        public List<Student> GetAll();
        public string GenerateCode();
        public void Save(Student student);
        public bool DuplicateCheck(string name, string pNumber);
        public void Delete(int id); 
        public bool ExestingCheck(int id);
        public void Update(Student student);
        public Student GetById(int id);
        public void Edit(Student student);
    }
}
