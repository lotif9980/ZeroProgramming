using ZPWEB.Data;
using ZPWEB.Enum;
using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public class ScheduleRepository:IScheduleRepository
    {
        protected readonly Db _db;
        public ScheduleRepository(Db db)
        {
            _db = db;
        }

       

        public List<Schedule> GetAll()
        {
           return _db.Schedules.ToList();
        }

        public string GenerateCode()
        {
           var lastScheduleCode=_db.Schedules.OrderByDescending(x=>x.Id).Select(x=>x.Code).FirstOrDefault();

            string newCode = "00001";
            if(!string.IsNullOrEmpty(lastScheduleCode) && int.TryParse(lastScheduleCode,out int lastCode))
            {
                newCode=(lastCode +1).ToString("D5");
            }

            return newCode;
        }

        public void Save(Schedule schedule)
        {
           _db.Schedules.Add(schedule);
        }

        public bool DuplicateCheck(string name)
        {
            return _db.Schedules.Any(x=>x.Name== name);
        }

        public void Delete(int id)
        {
            var data=_db.Schedules.Find(id);
            _db.Schedules.Remove(data);
        }

        public bool TransactionCheck(int id)
        {
            return _db.Schedules.Any(x=>x.Id== id);
        }

        public Schedule GetById(int id)
        {
            return _db.Schedules.Find(id);
        }

        public void Update(Schedule schedule)
        {
            _db.Schedules.Update(schedule);
        }
    }
}
