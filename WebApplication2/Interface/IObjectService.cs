using WebApplication2.Entitiy;
using WebApplication2.Response;

namespace WebApplication2.Interface
{
    public interface IObjectService
    {
        Result Add1(WktObj obj);
        Result AddRange1(List<WktObj> objs);
        Result Update1(WktObj obj);
        Result Delete1(int id);
        Result GetById1(int id);
        Result GetAll1();
    }
}
