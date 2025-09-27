using ZPWEB.Data;
using System.Diagnostics;

namespace ZPWEB.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        protected readonly Db db;

        public IInstractorRepository InstractorRepository { get; set; }
        public ICourseRepository CourseRepository { get; set; }
        public IMethodRepository MethodRepository {  get; set; }

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
            InstractorRepository = new InstractorRepository(db);
            CourseRepository = new CourseRepository(db);
            MethodRepository=new MethodRepository(db);

        }


    }
}
