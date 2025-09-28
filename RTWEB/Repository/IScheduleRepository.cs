using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IScheduleRepository
    {
        public List<Schedule> GetAll();
        public string GenerateCode();
    }
}
