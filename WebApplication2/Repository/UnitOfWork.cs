using WebApplication2.Interface;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepository<Wktobj> Wktobjs { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Wktobjs = new GenericRepository<Wktobj>(_context);
        }

        public int Commit()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
