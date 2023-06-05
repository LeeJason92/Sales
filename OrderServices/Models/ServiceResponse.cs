using System.Net;

namespace OrderServices.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; } = default;
        public int CurrentPage { get; set; } = 1;
        public int TotalPage { get; set; } = 1;
        public int DataPerPage { get; set; } = 10;
        public int TotalData { get; set; } = 0;
        public string FilterBy { get; set; } = "";
        public string OrderBy { get; set; } = "";
        public bool isSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public int StatusCode { get; set; } = 0;

        public void Fail(Exception e)
        {
            isSuccess = false;
            Message = "Error : " + e.Message;
            if (e.InnerException != null)
            {
                Message += " Inner Exception : " + e.InnerException.Message;
            }
            StatusCode = (int)HttpStatusCode.BadRequest;
        }

        public void Success()
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

    }
}
