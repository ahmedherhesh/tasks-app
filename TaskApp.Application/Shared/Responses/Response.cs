namespace TaskApp.Application.Shared.Responses
{
    public class Response<T>(T? data = default, bool success = true, string? message = null)
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool Success { get; set; } = success;
        public string? Message { get; set; } = message;

        public object? Data { get; set; } = data;
    }
}