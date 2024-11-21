using System.ComponentModel.DataAnnotations;

namespace CourtManagement.API.Models.Requests
{
    public class CreateCaseRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string JudgeId { get; set; }
    }
}
