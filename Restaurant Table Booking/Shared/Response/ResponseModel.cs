namespace Shared.Response
{
    public class ResponseModel
    {
        public bool IsSucceeded { get; set; }
        public object? Data { get; set; }
        public string? DescriptionMessage { get; set; }
    }
}
