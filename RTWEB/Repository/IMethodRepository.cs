using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface IMethodRepository
    {
        public IEnumerable<Method> GetAll();

    }
}
