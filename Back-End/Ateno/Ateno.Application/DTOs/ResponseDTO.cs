namespace Ateno.Application.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public string Message { get; set; }
    }

    public class ResponseObjectDTO<T>
    {
        public bool Success { get; set; }
        public T Object { get; set; }
        public string Message { get; set; }
    }
}
