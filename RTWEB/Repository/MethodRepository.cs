using ZPWEB.Data;
using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public class MethodRepository : IMethodRepository
    {
        protected readonly Db _db;
        public MethodRepository(Db db)
        {
            _db = db;
        }

        

        public IEnumerable<Method> GetAll()
        {
            return _db.Methods.ToList();
        }

        public void Save(Method method)
        {
           _db.Methods.Add(method);
        }

        public string GenerateCode()
        {
            var lastMethodCode=_db.Methods.OrderByDescending(x=>x.Id).Select(x=>x.Code).FirstOrDefault();

            string NewCode = "00001";

            if(!string.IsNullOrEmpty(lastMethodCode) && int.TryParse(lastMethodCode,  out int lastCode) )
            {
                NewCode =(lastCode + 1).ToString("D5");
            }

            return NewCode;
        }

        public bool DuplicateCheck(string name)
        {
          return _db.Methods.Any(x=>x.Name== name);
        }

        public void Delete(int id)
        {
            var data = _db.Methods.Find(id);
            _db.Remove(data);
        }

        public bool TransactionCheck(int id)
        {
            return _db.PaymentDetails.Any(x=>x.PaymentMethod== id);
        }

        public Method GetById(int id)
        {
            return _db.Methods.Find(id);
        }

        public void Update(Method method)
        {
           _db.Methods.Update(method);
        }
    }
}
