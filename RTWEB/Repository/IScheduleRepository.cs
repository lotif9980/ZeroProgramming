using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IScheduleRepository
    {
        public List<ScheduleVM> GetAll(); 
    }
}
