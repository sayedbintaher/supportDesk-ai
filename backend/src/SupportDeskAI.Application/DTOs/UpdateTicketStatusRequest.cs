using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Application.DTOs
{
    public class UpdateTicketStatusRequest
    {
        public TicketStatus Status {get; set; }
    }
}