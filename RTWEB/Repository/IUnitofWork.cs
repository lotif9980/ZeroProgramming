namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        
        IInstractorRepository InstractorRepository { get; }
        ICourseRepository CourseRepository { get; }
        IMethodRepository MethodRepository { get; }

        int Complete();
    }
}
