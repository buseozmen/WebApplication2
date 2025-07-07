using WebApplication2.Models;

namespace WebApplication2.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<Wktobj> Wktobjs { get; }
        int Commit();
    }
}
