using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface IInstractorRepository
    {
        public IQueryable<Instractor> GetAll();

    }
}
