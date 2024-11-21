using CourtManagement.API.Models.Requests;
using CourtManagement.API.Models.Responses;
using CourtManagement.API.Services.Interfaces;
using CourtManagement.API.Exceptions;
using System.Collections.Generic;
using CourtManagement.API.Models;


namespace CourtManagement.API.Services.Implementations
{
    public class CaseService : ICaseService
    {
        private readonly List<CaseResponse> _mockCases;

        public CaseService()
        {
            _mockCases = new List<CaseResponse>
        {
            new CaseResponse
            {
                Id = 2,
                CaseNumber = "2024/000",
                Title = "בדיקה",
                Status = 1,
                StatusDsc = "פעיל",
                JudgeId = "1",
                JudgeName = "בדיקה",
                OpenDate = DateTime.Now.AddDays(-1)
            },
            new CaseResponse
            {
                Id = 1,
                CaseNumber = "2024/001",
                Title = "תביעה אזרחית - לוי נגד כהן",
                Status = 1,
                StatusDsc = "פעיל",
                JudgeId = "2",
                JudgeName = "השופט אברהם לוי",
                OpenDate = DateTime.Now.AddDays(-30)
            },
            new CaseResponse
            {
                Id = 2,
                CaseNumber = "2024/002",
                Title = "בקשה - שמעון בעמ",
                Status = 2,
                StatusDsc = "ממתין לדיון",
                JudgeId = "3",
                JudgeName = "השופטת רחל כהן",
                OpenDate = DateTime.Now.AddDays(-15)
            },
            new CaseResponse
            {
                Id = 3,
                CaseNumber = "2024/003",
                Title = "ערעור פלילי - מדינת ישראל נ' ישראלי",
                Status = 3,
                StatusDsc = "סגור",
                JudgeId = "2",
                JudgeName = "השופט אברהם לוי",
                OpenDate = DateTime.Now.AddDays(-60)
            }
        };
        }

        public Task<IEnumerable<CaseResponse>> GetCasesAsync(CaseFilter filter)
        {
            IEnumerable<CaseResponse> query = _mockCases;

            if (!string.IsNullOrEmpty(filter.JudgeId))
            {
                query = query.Where(c => c.JudgeId == filter.JudgeId);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(c => c.Status == filter.Status.Value);
            }

            query = filter.SortBy?.ToLower() switch
            {
                "opendate" => filter.SortDirection?.ToLower() == "asc"
                    ? query.OrderBy(c => c.OpenDate)
                    : query.OrderByDescending(c => c.OpenDate),

                "casenumber" => filter.SortDirection?.ToLower() == "asc"
                    ? query.OrderBy(c => c.CaseNumber)
                    : query.OrderByDescending(c => c.CaseNumber),

                _ => query.OrderByDescending(c => c.OpenDate)
            };

            return Task.FromResult(query);
        }

        public Task<CaseResponse> GetCaseByIdAsync(int id)
        {
            var @case = _mockCases.FirstOrDefault(c => c.Id == id);
            if (@case == null)
                throw new NotFoundException($"תיק מספר {id} לא נמצא");
            return Task.FromResult(@case);
        }

        public Task<CaseResponse> CreateCaseAsync(CreateCaseRequest request)
        {
            var newCase = new CaseResponse
            {
                Id = _mockCases.Max(c => c.Id) + 1,
                CaseNumber = $"2024/{(_mockCases.Count + 1):D3}",
                Title = request.Title,
                Status = 1,
                StatusDsc = "פעיל",
                JudgeId = request.JudgeId,
                JudgeName = GetJudgeName(request.JudgeId),
                OpenDate = DateTime.Now
            };

            _mockCases.Add(newCase);
            return Task.FromResult(newCase);
        }

        public Task<CaseResponse> UpdateCaseAsync(int id, UpdateCaseRequest request)
        {
            var @case = _mockCases.FirstOrDefault(c => c.Id == id);
            if (@case == null)
                throw new NotFoundException($"תיק מספר {id} לא נמצא");

            @case.Status = request.Status;
            @case.StatusDsc = GetStatusDescription(request.Status);
            return Task.FromResult(@case);
        }

        private string GetJudgeName(string judgeId) => judgeId switch
        {
            "1" => "השופט אברהם לוי",
            "2" => "השופטת רחל כהן",
            "3" => "השופט דוד ישראלי",
            _ => "לא ידוע"
        };

        private string GetStatusDescription(int status) => status switch
        {
            1 => "פעיל",
            2 => "ממתין לדיון",
            3 => "סגור",
            _ => "לא ידוע"
        };
    }
}
