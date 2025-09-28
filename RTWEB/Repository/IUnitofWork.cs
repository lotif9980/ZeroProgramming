namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        
        IInstractorRepository InstractorRepository { get; }
        ICourseRepository CourseRepository { get; }
        IMethodRepository MethodRepository { get; }
        IScheduleRepository ScheduleRepository { get; }

        int Complete();
    }
}
