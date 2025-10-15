using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface IMethodRepository
    {
        public IEnumerable<Method> GetAll();
        public void Save(Method method);
        public string GenerateCode();
        public bool DuplicateCheck(string name);
        public void Delete(int id);
        public bool TransactionCheck(int id);
        public Method GetById(int id);
        public void Update(Method method);
    }
}
