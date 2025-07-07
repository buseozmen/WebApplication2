using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Response;
using WebApplication2.Interface;
using WebApplication2.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ObjectController : ControllerBase
{
    private readonly IEFObjectService _service;

    public ObjectController(IEFObjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public Result GetAll()
    {
        try
        {
            var data = _service.GetAll1();
            return new Result { Success = true, Message = "Veriler getirildi", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }

    [HttpGet("{id}")]
    public Result GetById(int id)
    {
        try
        {
            var data = _service.GetById1(id);
            return new Result { Success = true, Message = "Kayıt getirildi", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }

    [HttpPost]
    public Result Add([FromBody] WktCreateDto dto)
    {
        try
        {
            var data = _service.Add1(dto);
            return new Result { Success = true, Message = "Ekleme başarılı", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }

    [HttpPost("addrange")]
    public Result AddRange([FromBody] List<WktCreateDto> dtos)
    {
        try
        {
            var data = _service.AddRange1(dtos);
            return new Result { Success = true, Message = "Toplu ekleme başarılı", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }

    [HttpPut]
    public Result Update([FromBody] WktUpdateDto dto)
    {
        try
        {
            var data = _service.Update1(dto);
            return new Result { Success = true, Message = "Güncelleme başarılı", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }

    [HttpDelete("{id}")]
    public Result Delete(int id)
    {
        try
        {
            var data = _service.Delete1(id);
            return new Result { Success = true, Message = "Silme başarılı", Data = data };
        }
        catch (Exception ex)
        {
            return new Result { Success = false, Message = ex.Message };
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using WebApplication2.Entitiy;
//using WebApplication2.Response;
//using WebApplication2.Services;

//namespace WebApplication2.Controllers
//{
//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class ObjectController : ControllerBase
//    {
//        private readonly IPointListService _objectService;

//        public ObjectController(IPointListService objectService)
//        {
//            _objectService = objectService;
//        }

//        [HttpPost]
//        public Result Add(WktObj obj)
//        {
//            return _objectService.Add(obj);
//        }

//        [HttpPost]
//        public Result AddRange(List<WktObj> objs)
//        {
//            return _objectService.AddRange(objs);
//        }

//        [HttpPut]
//        public Result Update(WktObj obj)
//        {
//            return _objectService.Update(obj);
//        }

//        [HttpDelete]
//        public Result Delete(int id)
//        {
//            return _objectService.Delete(id);
//        }

//        [HttpGet]
//        public Result GetById(int id)
//        {
//            return _objectService.GetById(id);
//        }

//        [HttpGet]
//        public Result GetAll()
//        {
//            return _objectService.GetAll();
//        }
//    }
//}
