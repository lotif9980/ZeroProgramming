using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IEnrollmentRepository
    {
        public IEnumerable<IndexEnrollment> GetAll();
        public IEnumerable<IndexEnrollment> GetDueAmtount();
        public string CreateGenerateCode();
        public void Save(Enrollment enrollment);
        public bool DuplicateCheck(int studentId, int coursId);
        public Enrollment GetById(int id);
        public void Delete(int id);
        public void Update(Enrollment enrollment);
    }
}
