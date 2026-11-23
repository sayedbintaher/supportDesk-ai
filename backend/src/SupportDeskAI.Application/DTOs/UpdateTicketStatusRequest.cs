using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Application.DTOs
{
    public class UpdateTicketStatusRequest
    {
        public TicketStatus Status {get; set; }
    }
}