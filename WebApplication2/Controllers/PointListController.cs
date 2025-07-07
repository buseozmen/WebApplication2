using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entitiy;
using WebApplication2.Response;
using WebApplication2.Interface;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PointListController : ControllerBase
    {
        private readonly IPointListService _objectService;

        public PointListController(IPointListService objectService)
        {
            _objectService = objectService;
        }

        [HttpPost]
        public Result Add(WktObj obj)
        {
            return _objectService.Add(obj);
        }

        [HttpPost]
        public Result AddRange(List<WktObj> objs)
        {
            return _objectService.AddRange(objs);
        }

        [HttpPut]
        public Result Update(WktObj obj)
        {
            return _objectService.Update(obj);
        }

        [HttpDelete]
        public Result Delete(int id)
        {
            return _objectService.Delete(id);
        }

        [HttpGet]
        public Result GetById(int id)
        {
            return _objectService.GetById(id);
        }

        [HttpGet]
        public Result GetAll()
        {
            return _objectService.GetAll();
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using WebApplication2.Entitiy;
//using WebApplication2.Response;
//using WebApplication2.Services;

//[ApiController]
//[Route("api/[controller]")]
//public class PointListController : ControllerBase
//{
//    private readonly IPointListService _service;

//    public PointListController(IPointListService service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    public Result GetAll() => _service.GetAll();

//    [HttpGet("{id}")]
//    public Result GetById(int id) => _service.GetById(id);

//    [HttpPost]
//    public Result Add([FromBody] WktObj obj) => _service.Add(obj);

//    [HttpPost("addrange")]
//    public Result AddRange([FromBody] List<WktObj> objs) => _service.AddRange(objs);

//    [HttpPut]
//    public Result Update([FromBody] WktObj obj) => _service.Update(obj);

//    [HttpDelete("{id}")]
//    public Result Delete(int id) => _service.Delete(id);
//}


