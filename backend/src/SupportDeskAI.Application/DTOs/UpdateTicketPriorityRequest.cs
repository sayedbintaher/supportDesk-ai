using System.ComponentModel.DataAnnotations;
using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Application.DTOs
{
    public class UpdateTicketPriorityRequest
    {
        [Required]
        [EnumDataType(typeof(TicketPriority))]
        public TicketPriority? Priority { get; set; }
    }
}