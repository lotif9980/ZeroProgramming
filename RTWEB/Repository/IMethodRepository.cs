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

    }
}
