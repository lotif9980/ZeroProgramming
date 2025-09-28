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
    }
}
