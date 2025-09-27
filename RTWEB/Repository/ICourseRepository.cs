using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAll();
        public string GenerateCode();
        public void Save(Course course);
        public bool ExestingCheck(string name);
        public void Delete(int id);
    }
}
