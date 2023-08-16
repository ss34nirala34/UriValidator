using System.Net;

namespace UrlAccessValidator.BO
{
    public class ValidationResponseBO
    {
        public ValidationResponseBO()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
