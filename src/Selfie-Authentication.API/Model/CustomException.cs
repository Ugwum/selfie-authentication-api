using System.Runtime.Serialization;

namespace Selfie_Authentication.API.Model
{
    

    public class CustomException : Exception
    {
        public string code { get; set; }
        public CustomException(string? errorcode)
        {
            this.code = errorcode ?? "UNEXPECTED_ERROR";
        }

        public CustomException(string? errorcode, string? message) : base(message)
        {
            this.code = errorcode ?? "UNEXPECTED_ERROR";
        }

        public CustomException(string? errorcode, string? message, Exception? innerException) : base(message, innerException)
        {
            this.code = errorcode ?? "UNEXPECTED_ERROR";
        }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
