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
    }
}
