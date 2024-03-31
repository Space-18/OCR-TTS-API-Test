namespace OCR.Presentation.Errors
{
    internal class CodeError : CodeErrorResponse
    {
        public CodeError(int statusCode, string? message = null, object? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public object? Details { get; set; }
    }
}
