using System.Text.RegularExpressions;
using WebApplication2.Models;
using WebApplication2.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication2.Validator
{
    public static class WktValidator
    {
        private static readonly string _wktPattern = @"^(-?\d+\s+-?\d+\s*,\s*)*(-?\d+\s+-?\d+)$";

        public static Result Validate(Wktobj obj)
        {
            if (obj == null)
                return BaseValidator.Fail("Nesne null olamaz.");

            if (string.IsNullOrWhiteSpace(obj.Name))
                return BaseValidator.Fail("İsim boş olamaz.");

            if (string.IsNullOrWhiteSpace(obj.Wkt))
                return BaseValidator.Fail("WKT değeri boş olamaz.");

            if (!Regex.IsMatch(obj.Wkt.Trim(), _wktPattern))
                return BaseValidator.Fail("Geçerli bir WKT giriniz. Örnek: 30 10, 10 30, 40 40");

            return BaseValidator.Success("Geçerli.");
        }

    }
}

