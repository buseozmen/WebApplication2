using WebApplication2.DTOs;
using WebApplication2.Interface;
using WebApplication2.Models;
using WebApplication2.Validator;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite;

public class EFObjectService : IEFObjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly WKTReader _reader;

    public EFObjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _reader = new WKTReader();
    }

    public List<Wktobj> GetAll1()
    {
        return _unitOfWork.Wktobjs.GetAll().ToList();
    }

    public Wktobj GetById1(int id)
    {
        if (id <= 0)
            throw new Exception("Geçersiz ID.");

        var item = _unitOfWork.Wktobjs.GetById(id);
        if (item == null)
            throw new Exception("Kayıt bulunamadı.");

        return item;
    }

    public Wktobj Add1(WktCreateDto dto)
    {
        
        Geometry geometry;
        try
        {
            geometry = _reader.Read(dto.Wkt);
        }
        catch
        {
            throw new Exception("Geçersiz WKT formatı.");
        }

        var obj = new Wktobj
        {
            Name = dto.Name,
            Wkt = geometry
        };

        var validation = WktValidator.Validate(obj);
        if (!validation.Success)
            throw new Exception(validation.Message);


        _unitOfWork.Wktobjs.Add(obj);
        _unitOfWork.Commit();
        return obj;
    }

    public List<Wktobj> AddRange1(List<WktCreateDto> dtos)
    {
        var objs = new List<Wktobj>();

        foreach (var dto in dtos)
        {
            Geometry geometry;
            try
            {
                geometry = _reader.Read(dto.Wkt);
            }
            catch
            {
                throw new Exception($"Geçersiz WKT: {dto.Wkt}");
            }

            var obj = new Wktobj
            {
                Name = dto.Name,
                Wkt = geometry
            };

            var validation = WktValidator.Validate(obj);
            if (!validation.Success)
                throw new Exception(validation.Message);

            objs.Add(obj);
        }

        _unitOfWork.Wktobjs.AddRange(objs);
        _unitOfWork.Commit();
        return objs;
    }

    public Wktobj Update1(WktUpdateDto dto)
    {
        if (dto.ObjectId <= 0)
            throw new Exception("Geçersiz ObjectId.");

        var existing = _unitOfWork.Wktobjs.GetById(dto.ObjectId);
        if (existing == null)
            throw new Exception("Güncellenecek nesne bulunamadı.");

        Geometry geometry;
        try
        {
            geometry = _reader.Read(dto.Wkt);
        }
        catch
        {
            throw new Exception("Geçersiz WKT formatı.");
        }

        existing.Name = dto.Name;
        existing.Wkt = geometry;

        var validation = WktValidator.Validate(existing);
        if (!validation.Success)
            throw new Exception(validation.Message);

        _unitOfWork.Wktobjs.Update(existing);
        _unitOfWork.Commit();

        return existing;
    }

    public Wktobj Delete1(int id)
    {
        if (id <= 0)
            throw new Exception("Geçersiz ID.");

        var item = _unitOfWork.Wktobjs.GetById(id);
        if (item == null)
            throw new Exception("Silinecek nesne bulunamadı.");

        _unitOfWork.Wktobjs.Remove(item);
        _unitOfWork.Commit();

        return item;
    }
}




//using NetTopologySuite.IO;
//using WebApplication2.DTOs;
//using WebApplication2.Interface;
//using WebApplication2.Models;
//using WebApplication2.Validator;

//public class EFObjectService : IEFObjectService
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly WKTReader _reader;

//    public EFObjectService(IUnitOfWork unitOfWork)
//    {
//        _unitOfWork = unitOfWork;
//        _reader = new WKTReader();
//    }

//    public List<Wktobj> GetAll1()
//    {
//        return _unitOfWork.Wktobjs.GetAll().ToList();
//    }

//    public Wktobj GetById1(int id)
//    {
//        if (id <= 0)
//            throw new Exception("Geçersiz ID.");

//        var item = _unitOfWork.Wktobjs.GetById(id);
//        if (item == null)
//            throw new Exception("Kayıt bulunamadı.");

//        return item;
//    }

//    public Wktobj Add1(WktCreateDto dto)
//    {
//        var obj = new Wktobj
//        {
//            Name = dto.Name,
//            Wkt = dto.Wkt
//        };


//        var validation = WktValidator.Validate(obj);
//        if (!validation.Success)
//            throw new Exception(validation.Message);

//        _unitOfWork.Wktobjs.Add(obj);
//        _unitOfWork.Commit();
//        return obj;
//    }

//    public List<Wktobj> AddRange1(List<WktCreateDto> dtos)
//    {
//        var objs = new List<Wktobj>();

//        foreach (var dto in dtos)
//        {
//            var obj = new Wktobj
//            {
//                Name = dto.Name,
//                Wkt = dto.Wkt
//            };

//            var validation = WktValidator.Validate(obj);
//            if (!validation.Success)
//                throw new Exception(validation.Message);

//            objs.Add(obj);
//        }

//        _unitOfWork.Wktobjs.AddRange(objs);
//        _unitOfWork.Commit();
//        return objs;
//    }

//    public Wktobj Update1(WktUpdateDto dto)
//    {
//        if (dto.ObjectId <= 0)
//            throw new Exception("Geçersiz ObjectId.");

//        var existing = _unitOfWork.Wktobjs.GetById(dto.ObjectId);
//        if (existing == null)
//            throw new Exception("Güncellenecek nesne bulunamadı.");

//        existing.Name = dto.Name;
//        existing.Wkt = dto.Wkt;

//        var validation = WktValidator.Validate(existing);
//        if (!validation.Success)
//            throw new Exception(validation.Message);

//        _unitOfWork.Wktobjs.Update(existing);
//        _unitOfWork.Commit();

//        return existing;
//    }

//    public Wktobj Delete1(int id)
//    {
//        if (id <= 0)
//            throw new Exception("Geçersiz ID.");

//        var item = _unitOfWork.Wktobjs.GetById(id);
//        if (item == null)
//            throw new Exception("Silinecek nesne bulunamadı.");

//        _unitOfWork.Wktobjs.Remove(item);
//        _unitOfWork.Commit();

//        return item;
//    }
//}







//using WebApplication2.Interface;
//using WebApplication2.Models;
//using WebApplication2.Validator;
//using WebApplication2.DTOs;

//public class EFObjectService : IEFObjectService
//{
//    private readonly AppDbContext _context;
//    public EFObjectService(AppDbContext context)
//    {
//        _context = context;
//    }

//    public List<Wktobj> GetAll1()
//    {
//        return _context.Wktobjs.ToList();
//    }

//    public Wktobj GetById1(int id)
//    {
//        if (id <= 0)
//            throw new Exception("Geçersiz ID.");

//        var item = _context.Wktobjs.Find(id);
//        if (item == null)
//            throw new Exception("Kayıt bulunamadı.");

//        return item;
//    }

//    public Wktobj Add1(WktCreateDto dto)
//    {
//        var obj = new Wktobj
//        {
//            Name = dto.Name,
//            Wkt = dto.Wkt
//        };

//        var validation = WktValidator.Validate(obj);
//        if (!validation.Success)
//            throw new Exception(validation.Message);

//        _context.Wktobjs.Add(obj);
//        _context.SaveChanges();
//        return obj;
//    }

//    public List<Wktobj> AddRange1(List<WktCreateDto> dtos)
//    {
//        var objects = new List<Wktobj>();

//        foreach (var dto in dtos)
//        {
//            var obj = new Wktobj 
//            { 
//                Name = dto.Name, 
//                Wkt = dto.Wkt 
//            };

//            var validation = WktValidator.Validate(obj);
//            if (!validation.Success)
//                throw new Exception(validation.Message);
//            objects.Add(obj);
//        }

//        _context.Wktobjs.AddRange(objects);
//        _context.SaveChanges();
//        return objects;
//    }

//    public Wktobj Update1(WktUpdateDto dto)
//    {
//        if (dto.ObjectId <= 0)
//            throw new Exception("Geçersiz ObjectId.");

//        var existing = _context.Wktobjs.Find(dto.ObjectId);
//        if (existing == null)
//            throw new Exception("Güncellenecek nesne bulunamadı.");

//        existing.Name = dto.Name;
//        existing.Wkt = dto.Wkt;

//        var validation = WktValidator.Validate(existing);
//        if (!validation.Success)
//            throw new Exception(validation.Message);

//        _context.SaveChanges();
//        return existing;
//    }

//    public Wktobj Delete1(int id)
//    {
//        if (id <= 0)
//            throw new Exception("Geçersiz ID.");

//        var item = _context.Wktobjs.Find(id);
//        if (item == null)
//            throw new Exception("Silinecek nesne bulunamadı.");

//        _context.Wktobjs.Remove(item);
//        _context.SaveChanges();

//        return item;
//    }
//}
