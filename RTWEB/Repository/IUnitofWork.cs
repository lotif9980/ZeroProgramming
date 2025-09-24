namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        //IDomainRepository DomainRepository { get; }


        int Complete();
    }
}
