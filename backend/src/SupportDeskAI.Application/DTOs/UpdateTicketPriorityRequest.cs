using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Application.DTOs
{
    public class UpdateTicketPriorityRequest
    {
        public TicketPriority Priority { get; set; }
    }
}