namespace TaskApp.Application.Shared.Responses
{
    public class Response<T>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public object Data { get; set; }

        public Response(T data, bool success = true, string message = "")
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}