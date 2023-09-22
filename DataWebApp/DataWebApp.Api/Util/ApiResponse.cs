namespace DataWebApp.Api.Util
{
    public class ApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Payload { get; set; }
    }
}
