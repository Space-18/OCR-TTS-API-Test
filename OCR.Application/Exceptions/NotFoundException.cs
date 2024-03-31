namespace OCR.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string className, object key) : base($"Entity \"{className}\" ({key}) no fue encontrado.")
        {
        }
    }
}
