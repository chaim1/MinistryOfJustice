using System.Diagnostics;

namespace CourtManagement.API.Models.Responses
{
    public class ErrorResponse
    {
        public string Message { get; }
        public string RequestId { get; }

        public ErrorResponse(string message)
        {
            Message = message;
            RequestId = Activity.Current?.Id;
        }
    }
}
