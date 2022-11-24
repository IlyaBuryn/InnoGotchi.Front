namespace InnoGotchi.Http.Models
{
    public class ResponseModel<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; }

        public ResponseModel(T? value, string? errorMessage = null)
        {
            Value = value;
            ErrorMessage = errorMessage;
        }

    }
}
