using ZPWEB.Data;
using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public class InstractorRepository : IInstractorRepository
    {
        protected readonly Db _db;
        
        public InstractorRepository(Db db)
        {
            _db = db;
        }

      

        public IQueryable<Instractor> GetAll()
        {
            return _db.Instractors;
        }

        public void Save(Instractor instractor)
        {
           _db.Instractors.Add(instractor);
        }
        public bool checkDuplicate(string name,string contactNo)
        {
           return _db.Instractors.Any(x=>x.Name == name && x.ContactNo== contactNo);
        }

        public void Delete(int id)
        {
            var data = _db.Instractors.Find(id);
            _db.Remove(data);
        }

        public string GenerateCode()
        {
            var lastCustomerCode = _db.Instractors.OrderByDescending(x => x.Id).Select(x => x.Code).FirstOrDefault();

            string newCode = "00001";

            if(!string.IsNullOrEmpty(lastCustomerCode) && int.TryParse(lastCustomerCode,out int lastCode))
            {
                newCode = (lastCode + 1).ToString("D5");
            }

            return newCode;
        }
    }
}
