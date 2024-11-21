using System.ComponentModel.DataAnnotations;

namespace CourtManagement.API.Models.Requests
{
    public class UpdateCaseRequest
    {
        [Required]
        public int Status { get; set; }
    }
}
