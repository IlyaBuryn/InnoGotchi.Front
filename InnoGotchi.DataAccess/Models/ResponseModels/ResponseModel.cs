using InnoGotchi.DataAccess.Extensions;

namespace InnoGotchi.DataAccess.Models.ResponseModels
{
    public class ResponseModel<T>
    {
        public T? Value { get; set; }
        public IList<string> Errors { get; set; }

        public string Error
        {
            get
            {
                if (Errors != null && Errors.Count != 0)
                {
                    return Errors[0];
                }
                return null;
            }
        }

        public ResponseModel(T? value)
        {
            Value = value;
            Errors = new List<string>();
        }

        public ResponseModel(T? value, string? message)
        {
            Value = value;
            Errors = new List<string>
            {
                message == null ? string.Empty : message,
            };
        }

        public ResponseModel()
        {
            Errors = new List<string>();
        }

        public ResponseModel<T> SetAndReturnError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public bool ItHasErrors()
        {
            if (Errors == null)
            {
                return true;
            }
            else if (Errors.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ItHasErrorsOrValueIsNull()
        {
            if (Errors == null)
            {
                return true;
            }
            else if (Errors.Count != 0)
            {
                return true;
            }
            else if (Value == null)
            {
                Errors.Add(this.ValueIsNullError(nameof(Value)));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
