namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        
        IInstractorRepository InstractorRepository { get; }
        ICourseRepository CourseRepository { get; }

        int Complete();
    }
}
