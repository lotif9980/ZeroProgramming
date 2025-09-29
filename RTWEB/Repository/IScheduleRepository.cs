using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IScheduleRepository
    {
        public List<Schedule> GetAll();
        public string GenerateCode();
        public void Save(Schedule schedule);
        public bool DuplicateCheck(string name);
        public void Delete(int id);
    }
}
