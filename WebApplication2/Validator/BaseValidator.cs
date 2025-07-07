using WebApplication2.Response;

namespace WebApplication2.Validator
{
    public class BaseValidator
    {
        public static Result Success(string message, object data = null)
        {
            return new Result { Success = true, Message = message, Data = data };
        }

        public static Result Fail(string message)
        {
            return new Result { Success = false, Message = message, Data = null };
        }
    }
}
