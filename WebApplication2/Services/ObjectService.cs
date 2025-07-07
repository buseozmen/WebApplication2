using Npgsql;
using System.Text.RegularExpressions;
using WebApplication2.Data;
using WebApplication2.Entitiy;
using WebApplication2.Response;
using WebApplication2.Interface;

namespace WebApplication2.Services
{
    public class ObjectService : IObjectService
    {
        private readonly DbHelper _db;
        private readonly string wktPattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";

        public ObjectService(DbHelper db)
        {
            _db = db;
        }

        public Result GetAll1()
        {
            var list = new List<WktObj>();
            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM wktObj", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new WktObj
                {
                    ObjectId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Wkt = reader.GetString(2)
                });
            }

            return new Result
            {
                Success = true,
                Message = "Veriler başarıyla getirildi.",
                Data = list
            };
        }

        public Result GetById1(int id)
        {
            if (id <= 0)
                return Fail("Geçersiz ID.");

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM wktObj WHERE \"ObjectId\" = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return Fail("Kayıt bulunamadı.");

            var obj = new WktObj
            {
                ObjectId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Wkt = reader.GetString(2)
            };

            return Success("Kayıt bulundu.", obj);
        }

        public Result Add1(WktObj obj)
        {
            var validation = Validate(obj);
            if (!validation.Success)
                return validation;

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("INSERT INTO wktObj (\"Name\", \"Wkt\") VALUES (@name, @wkt)", conn);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@wkt", obj.Wkt);
            cmd.ExecuteNonQuery();

            return Success("Ekleme başarılı.");
        }

        public Result AddRange1(List<WktObj> objs)
        {
            foreach (var obj in objs)
            {
                var validation = Validate(obj);
                if (!validation.Success)
                    return validation;
            }

            using var conn = _db.GetConnection();
            conn.Open();

            foreach (var obj in objs)
            {
                var cmd = new NpgsqlCommand("INSERT INTO wktObj (\"Name\", \"Wkt\") VALUES (@name, @wkt)", conn);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@wkt", obj.Wkt);
                cmd.ExecuteNonQuery();
            }

            return Success("Toplu ekleme başarılı.");
        }

        public Result Update1(WktObj obj)
        {
            if (obj.ObjectId <= 0)
                return Fail("Geçersiz ObjectId.");

            var mevcut = GetById1(obj.ObjectId);
            if (!mevcut.Success)
                return Fail("Güncellenecek nesne bulunamadı.");

            var validation = Validate(obj);
            if (!validation.Success)
                return validation;

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("UPDATE wktObj SET \"Name\" = @name, \"Wkt\" = @wkt WHERE \"ObjectId\" = @id", conn);
            cmd.Parameters.AddWithValue("@name", obj.Name);
            cmd.Parameters.AddWithValue("@wkt", obj.Wkt);
            cmd.Parameters.AddWithValue("@id", obj.ObjectId);
            cmd.ExecuteNonQuery();

            return Success("Güncelleme başarılı.");
        }

        public Result Delete1(int id)
        {
            if (id <= 0)
                return Fail("Geçersiz ID.");

            var mevcut = GetById1(id);
            if (!mevcut.Success)
                return Fail("Silinecek nesne bulunamadı.");

            using var conn = _db.GetConnection();
            conn.Open();

            var cmd = new NpgsqlCommand("DELETE FROM wktObj WHERE \"ObjectId\" = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            return Success("Silme başarılı.");
        }

        // Adonet için validate
        private Result Validate(WktObj obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Name))
                return Fail("Name alanı boş olamaz.");

            if (string.IsNullOrWhiteSpace(obj.Wkt))
                return Fail("Wkt alanı boş olamaz.");

            if (!Regex.IsMatch(obj.Wkt.Trim(), wktPattern))
                return Fail("Geçerli bir WKT giriniz. Örnek: 30 10, 10 30, 40 40");

            return Success("Doğrulama başarılı.", obj);
        }

        private Result Success(string message, object data = null)
        {
            return new Result { Success = true, Message = message, Data = data };
        }

        private Result Fail(string message)
        {
            return new Result { Success = false, Message = message, Data = null };
        }
    }
}
