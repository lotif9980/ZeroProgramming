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
        public IScheduleRepository ScheduleRepository { get; set; }
        public IStudentRepository StudentRepository {  get; set; }
        public IEnrollmentRepository EnrollmentRepository {  get; set; }

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
            ScheduleRepository=new ScheduleRepository(db);
            StudentRepository=new StudentRepository(db);
            EnrollmentRepository= new EnrollmentRepository(db);

        }


    }
}
