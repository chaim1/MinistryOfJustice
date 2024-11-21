using CourtManagement.API.Models;
using CourtManagement.API.Models.Requests;
using CourtManagement.API.Models.Responses;

namespace CourtManagement.API.Services.Interfaces
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseResponse>> GetCasesAsync(CaseFilter filter);
        Task<CaseResponse> GetCaseByIdAsync(int id);
        Task<CaseResponse> CreateCaseAsync(CreateCaseRequest request);
        Task<CaseResponse> UpdateCaseAsync(int id, UpdateCaseRequest request);
    }
}
