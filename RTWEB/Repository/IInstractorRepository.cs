using ZPWEB.Models;

namespace ZPWEB.Repository
{
    public interface IInstractorRepository
    {
        public IQueryable<Instractor> GetAll();
        public void Save(Instractor instractor);
        public bool checkDuplicate(string name,string contactNo);
        public void Delete(int id);
        public string GenerateCode();

    }
}
