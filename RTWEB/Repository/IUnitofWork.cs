namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        
        IInstractorRepository InstractorRepository { get; }

        int Complete();
    }
}
