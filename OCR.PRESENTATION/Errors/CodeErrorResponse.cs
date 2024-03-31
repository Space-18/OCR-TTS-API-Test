namespace OCR.Presentation.Errors
{
    internal class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } = null;

        public CodeErrorResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private static string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Error en la solicitud.",
                401 => "No autenticado.",
                403 => "No autorizado",
                404 => "El recurso no fue encontrado.",
                415 => "Formato multimedia no doportado.",
                418 => "Soy una tetera.",
                500 => "Error interno del servidor.",
                _ => string.Empty
            };
        }
    }
}
