using WebApplication2.DTOs;
using WebApplication2.Models;
using WebApplication2.Response;

namespace WebApplication2.Interface
{
    public interface IEFObjectService
    {
        List<Wktobj> GetAll1();
        Wktobj GetById1(int id);
        Wktobj Add1(WktCreateDto dto);
        List<Wktobj> AddRange1(List<WktCreateDto> dtos);
        Wktobj Update1(WktUpdateDto dto);
        Wktobj Delete1(int id);

    }
}
