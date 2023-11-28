namespace Ateno.API.ViewObjects
{
    public class APIResponseVO
    {
        public bool Success { get; set; }
        public int Number { get; set; }
        public object Object { get; set; }
        public string Message { get; set; }

        public APIResponseVO()
        {
        }
        public APIResponseVO(string message, int number, object @object, bool success)
        {
            Message = message;
            Number = number;
            Object = @object;
            Success = success;
        }

        public static APIResponseVO Ok(string message = null, object obj = null, int number = 0)
        {
            return new APIResponseVO(message, number, obj, true);
        }

        public static APIResponseVO Fail(string message = null, object obj = null, int number = 0)
        {
            return new APIResponseVO(message, number, obj, false);
        }
    }
}
