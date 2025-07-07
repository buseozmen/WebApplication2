using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entitiy;
using WebApplication2.Response;
using WebApplication2.Interface;

namespace WebApplication2.Services
{
    public class PointListService : IPointListService
    {
        private static readonly List<WktObj> _objectsList = new();
        private static int _idCounter = 1;

        [HttpPost]
        public Result Add(WktObj obj)
        {
            var result = new Result { Success = false };
            

            if (obj == null)
            {
                result.Message = "Nesne boş olamaz.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(obj.Name) || obj.Name.Length > 100)
            {
                result.Message = "İsim boş olamaz ve 100 karakterden uzun olmamalı.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(obj.Wkt))
            {
                result.Message = "WKT boş olamaz.";
                return result;
            }

            var pattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";

            
            if (!Regex.IsMatch(obj.Wkt.Trim(), pattern))
            {
                result.Message = "Geçerli bir WKT giriniz. Örnek: 30 10, 10 30, 40 40";
                return result;
            }



            obj.ObjectId = _idCounter++;
            _objectsList.Add(obj);

            result.Success = true;
            result.Message = "Başarılı şekilde eklendi.";
            result.Data = obj;

            return result;
        }

        public Result AddRange(List<WktObj> objs)
        {
            var result = new Result { Success = false };

            var pattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";

            for (int i = 0; i < objs.Count; i++)
            {
                 if (!Regex.IsMatch(objs[i].Wkt.Trim(), pattern))
                 {
                     result.Message = "Geçerli bir WKT giriniz. Örnek: 30 10, 10 30, 40 40";
                     return result;
                 }
            }
           
            if (objs == null || !objs.Any())
            {
                result.Message = "Eklenecek nesne listesi boş olamaz.";
                return result;
            }

            foreach (var obj in objs)
            {
                if (string.IsNullOrWhiteSpace(obj.Name) || obj.Name.Length > 100)
                {
                    result.Message = $"'{obj.Name}' isimli nesne geçersiz.";
                    return result;
                }

                if (string.IsNullOrWhiteSpace(obj.Wkt))
                {
                    result.Message = $"'{obj.Name}' isimli nesnenin WKT bilgisi boş.";
                    return result;
                }
            }

            foreach (var obj in objs)
            {
                obj.ObjectId = _idCounter++;
                _objectsList.Add(obj);
            }

            result.Success = true;
            result.Message = $"{objs.Count} adet nesne başarıyla eklendi.";
            result.Data = objs;

            return result;
        }

        public Result Update(WktObj obj)
        {
            var result = new Result { Success = false };

            if (obj == null)
            {
                result.Message = "Güncellenecek nesne boş olamaz.";
                return result;
            }

            var mevcutWkt = _objectsList.FirstOrDefault(o => o.ObjectId == obj.ObjectId);
            if (mevcutWkt == null)
            {
                result.Message = "Güncellenecek nesne bulunamadı.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(obj.Name) || obj.Name.Length > 100)
            {
                result.Message = "İsim boş olamaz ve 100 karakterden uzun olmamalı.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(obj.Wkt))
            {
                result.Message = "WKT boş olamaz.";
                return result;
            }

            var pattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";


            if (!Regex.IsMatch(obj.Wkt.Trim(), pattern))
            {
                result.Message = "Geçerli bir WKT giriniz. Örnek: 30 10, 10 30, 40 40";
                return result;
            }

            // Güncelle
            mevcutWkt.Name = obj.Name;
            mevcutWkt.Wkt = obj.Wkt;

            result.Success = true;
            result.Message = "Güncelleme başarılı.";
            result.Data = mevcutWkt;

            return result;
        }

        public Result Delete(int id)
        {
            var result = new Result { Success = false };

            var obj = _objectsList.FirstOrDefault(o => o.ObjectId == id);
            if (obj == null)
            {
                result.Message = "Silinecek nesne bulunamadı.";
                return result;
            }

            _objectsList.Remove(obj);

            result.Success = true;
            result.Message = "Silme işlemi başarılı.";
            result.Data = obj;

            return result;
        }

        public Result GetById(int id)
        {
            var result = new Result { Success = false };

            var obj = _objectsList.FirstOrDefault(o => o.ObjectId == id);
            if (obj == null)
            {
                result.Message = "Nesne bulunamadı.";
                return result;
            }

            result.Success = true;
            result.Message = "Nesne bulundu.";
            result.Data = obj;

            return result;
        }

        public Result GetAll()
        {
            return new Result
            {
                Success = true,
                Message = "Veriler getirildi.",
                Data = _objectsList
            };
        }


    }
}
