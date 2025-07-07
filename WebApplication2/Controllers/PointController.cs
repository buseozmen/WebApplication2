using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entitiy;
using WebApplication2.Response;
using Point = WebApplication2.Entitiy.Point;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PointController : ControllerBase
    {
        private static readonly List<Point> pointList = new List<Point>();
        private static int _id = 1;

        [HttpPost]
        public Result Add(string name, int x, int y)
        {
            var result = new Result();
            result.Success = false;

            if (string.IsNullOrEmpty(name))
            {
                result.Message = "İsim boş olamaz.";
                return result;
            }

            if (name.Length > 100)
            {
                result.Message = "İsim 100 karakterden fazla olamaz.";
                return result;
            }

            if (x < -100000 || x > 100000)
            {
                result.Message = "X koordinatı geçersiz aralıkta.";
                return result;
            }

            if (y < -100000 || y > 100000)
            {
                result.Message = "Y koordinatı geçersiz aralıkta.";
                return result;
            }

            var point = new Point
            {
                Id = _id,
                Name = name,
                X = x,
                Y = y
            };

            pointList.Add(point);
            _id++;
            result.Success = true;
            result.Message = "Başarılı bir şekilde eklendi.";
            result.Data = point;

            return result;
        }

        // Birden fazla noktayı toplu ekleme
        [HttpPost]
        public Result AddRange(List<Point> points)
        {
            var result = new Result();
            result.Success = false;

            if (points == null || points.Count == 0)
            {
                result.Message = "Eklenecek nokta listesi boş olamaz.";
                return result;
            }

            var addedPoints = new List<Point>();

            // Her nokta için validation yap ve ekle
            foreach (var p in points)
            {
                // İsim boş veya uzun mu?
                if (string.IsNullOrWhiteSpace(p.Name) || p.Name.Length > 100)
                {
                    result.Message = $"'{p.Name}' isimli nokta geçersiz. İsim boş olmamalı ve 100 karakterden kısa olmalı.";
                    return result;
                }

                // Koordinatlar makul aralıkta mı?
                if (p.X < -100000 || p.X > 100000 || p.Y < -100000 || p.Y > 100000)
                {
                    result.Message = $"'{p.Name}' isimli noktanın koordinatları geçersiz aralıkta.";
                    return result;
                }

                // Yeni id ata ve ekle
                p.Id = _id;
                _id++;
                addedPoints.Add(p);
            }

            // Tüm geçerli noktaları listeye ekle
            pointList.AddRange(addedPoints);

            result.Success = true;
            result.Message = $"{addedPoints.Count} adet nokta başarıyla eklendi.";
            result.Data = addedPoints;

            return result;
        }


        [HttpGet]
        public List<Point> GetAll()
        {
            return pointList;
        }

        [HttpGet]
        public Result GetById( int getId)
        {
            var result = new Result();
            result.Success = false;
            for (int i =0; i < pointList.Count; i++)
            {
                if (pointList[i].Id == getId)
                {
                    result.Success = true;
                    result.Message = "Veri bulundu.";
                    result.Data = pointList[i];
                }

                result.Message = "Veri bulunamadı.";
                result.Data = null;
            }
            return result;
        }

        [HttpPut]
        public Result Update(Point updatedPoint)
        {
            var result = new Result();
            result.Success = false;

            if (updatedPoint == null)
            {
                result.Message = "Güncellenecek veri boş olamaz.";
                return result;
            }

            // İsim kontrolü
            if (string.IsNullOrWhiteSpace(updatedPoint.Name))
            {
                result.Message = "İsim boş olamaz.";
                return result;
            }

            if (updatedPoint.Name.Length > 100)
            {
                result.Message = "İsim 100 karakterden fazla olamaz.";
                return result;
            }

            // Koordinat aralığı kontrolü
            if (updatedPoint.X < -100000 || updatedPoint.X > 100000)
            {
                result.Message = "X koordinatı geçersiz aralıkta.";
                return result;
            }

            if (updatedPoint.Y < -100000 || updatedPoint.Y > 100000)
            {
                result.Message = "Y koordinatı geçersiz aralıkta.";
                return result;
            }

            // Güncellenecek noktayı bul
            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i].Id == updatedPoint.Id)
                {
                    // Güncelle
                    pointList[i] = updatedPoint;
                    result.Success = true;
                    result.Message = "Veri güncellendi.";
                    result.Data = updatedPoint;
                    return result;
                }
            }

            result.Message = "Güncellenecek veri bulunamadı.";
            result.Data = null;
            return result;
        }

        [HttpDelete]
        public Result Delete(int id)
        {
            var result = new Result();
            result.Success = false;

            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i].Id == id)
                {
                    var deletedPoint = pointList[i];
                    pointList.RemoveAt(i);
                    result.Success = true;
                    result.Message = "Veri başarıyla silindi.";
                    result.Data = deletedPoint;
                    return result;
                }
            }

            result.Message = "Silinecek veri bulunamadı.";
            result.Data = null;
            return result;
        }




    }
}
