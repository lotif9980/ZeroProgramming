using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAll();
        public IEnumerable<Course> ActiveGetAll();
        public string GenerateCode();
        public void Save(Course course);
        public bool ExestingCheck(string name);
        public void Delete(int id);
        public Course GetById(int id);
        public bool TransactionCheck(int id);
        public void UpdateStatus(int id);
        public void Update(Course course);


    }
}
