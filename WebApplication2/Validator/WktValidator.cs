using System.Text.RegularExpressions;
using NetTopologySuite.IO;
using WebApplication2.DTOs;
using WebApplication2.Models;
using WebApplication2.Response;

namespace WebApplication2.Validator
{
    public static class WktValidator
    {
        //private static readonly string _wktPattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";
        private static readonly string _wktPattern =
            @"^(POINT\s*\(\s*-?\d+(\.\d+)?\s+-?\d+(\.\d+)?\s*\)|" +
            @"LINESTRING\s*\(\s*(-?\d+(\.\d+)?\s+-?\d+(\.\d+)?\s*,\s*)*(-?\d+(\.\d+)?\s+-?\d+(\.\d+)?)\s*\)|" +
            @"POLYGON\s*\(\(\s*(-?\d+(\.\d+)?\s+-?\d+(\.\d+)?\s*,\s*)*(-?\d+(\.\d+)?\s+-?\d+(\.\d+)?)*\s*\)\))$";

        public static Result Validate(Wktobj obj)
        {
            if (obj == null)
                return BaseValidator.Fail("Nesne null olamaz.");

            if (string.IsNullOrWhiteSpace(obj.Name))
                return BaseValidator.Fail("İsim boş olamaz.");

            if (obj.Wkt == null)
                return BaseValidator.Fail("WKT değeri boş olamaz.");

            var writer = new WKTWriter();
            var wktText = writer.Write(obj.Wkt);

            if (!Regex.IsMatch(wktText.Trim(), _wktPattern))
                return BaseValidator.Fail("Geçerli bir WKT giriniz. Örnek: POINT (30 10), LINESTRING (30 10, 10 30), POLYGON ((30 10, 40 40, 30 10))");

            return BaseValidator.Success("Geçerli.");
        }

    }
}

