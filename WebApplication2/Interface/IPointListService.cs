using WebApplication2.Entitiy;
using WebApplication2.Response;

namespace WebApplication2.Interface
{
    public interface IPointListService
    {
        Result Add(WktObj obj);
        Result AddRange(List<WktObj> objs);
        Result Update(WktObj obj);
        Result Delete(int id);
        Result GetById(int id);
        Result GetAll();
    }
}
