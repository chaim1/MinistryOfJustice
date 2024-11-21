using CourtManagement.API.Models;
using CourtManagement.API.Models.Events;
using CourtManagement.API.Models.Requests;
using CourtManagement.API.Models.Responses;
using CourtManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CourtManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasesController : BaseController
    {
        private readonly ICaseService _caseService;
        private readonly INotificationService _notificationService;

        public CasesController(
            ICaseService caseService,
            IMessageBusService messageBus,
            INotificationService notificationService,
            ILogger<CasesController> logger)
            : base(logger, messageBus)
        {
            _caseService = caseService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<CaseResponse>>> GetCases(
            [FromQuery] string judgeId = null,
            [FromQuery] int? status = null,
            [FromQuery] string sortBy = "openDate",
            [FromQuery] string sortDirection = "desc")
        {
            return HandleOperation(() => _caseService.GetCasesAsync(
                new CaseFilter
                {
                    JudgeId = judgeId,
                    Status = status,
                    SortBy = sortBy,
                    SortDirection = sortDirection
                }));
        }

        [HttpGet("{id}")]
        public Task<ActionResult<CaseResponse>> GetCase(int id)
        {
            return HandleOperation(() => _caseService.GetCaseByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<CaseResponse>> CreateCase(CreateCaseRequest request)
        {
            try
            {
                var result = await HandleOperation(() => _caseService.CreateCaseAsync(request));

                if (result.Value != null)
                {
                    _ = SendNotificationsAsync(result.Value)
                        .ContinueWith(task =>
                        {
                            if (task.IsFaulted)
                            {
                                _logger.LogError(task.Exception,
                                    "Failed to send notifications for case {CaseId}",
                                    result.Value.Id);
                            }
                        });

                    return result;
                }

                return BadRequest("Failed to create case");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateCase");
                throw;
            }
        }

        private async Task SendNotificationsAsync(CaseResponse @case)
        {
            try
            {
                var notifications = new List<NotificationMessage>
                {
                    new NotificationMessage
                    {
                        EventName = "case.created",
                        Data = new CaseCreatedEvent(@case)
                    },
                    new NotificationMessage
                    {
                        EventName = "case.secretary.review",
                        Data = new SecretaryReviewEvent(@case)
                    }
                };

                var tasks = notifications.Select(n =>
                    _messageBus.PublishAsync(n.EventName, n.Data));

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notifications");
                throw;
            }
        }

        [HttpPut("{id}")]
        public Task<ActionResult<CaseResponse>> UpdateCase(int id, UpdateCaseRequest request)
        {
            return HandleOperation(() => _caseService.UpdateCaseAsync(id, request));
        }
    }

    public class NotificationMessage
    {
        public string EventName { get; set; }
        public object Data { get; set; }
    }
}
