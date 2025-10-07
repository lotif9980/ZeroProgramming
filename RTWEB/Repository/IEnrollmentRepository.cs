using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IEnrollmentRepository
    {
        public IEnumerable<IndexEnrollment> GetAll();
        public string CreateGenerateCode();
        public void Save(Enrollment enrollment);
        public bool DuplicateCheck(int studentId, int coursId);

        public void Delete(int id);
    }
}
