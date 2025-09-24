using ZPWEB.Data;
using System.Diagnostics;

namespace ZPWEB.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        protected readonly Db db;

    

        //public IDomainRepository DomainRepository { get; set; }
        

        public int Complete()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public UnitOfWork(Db db)
        {
            this.db = db;
            //DomainRepository = new DomainRepository(db);
           

        }
      
    }
}
