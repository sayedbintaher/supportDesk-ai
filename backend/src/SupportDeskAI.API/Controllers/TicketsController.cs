using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupportDeskAI.Application.DTOs;
using SupportDeskAI.Application.Interfaces;

namespace SupportDeskAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var createdBy = Guid.Parse(
            "11111111-1111-1111-1111-111111111111");

            var ticketId = await _ticketService.CreateTicketAsync(request, createdBy, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new
            {
                Id = ticketId,
                Message = "Ticket created successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets(CancellationToken cancellationToken)
        {
            var tickets = await _ticketService.GetAllTicketsAsync(cancellationToken);
            return Ok(tickets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTicketById(Guid id, CancellationToken cancellationToken)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id, cancellationToken);

            if (ticket is null)
            {
                return NotFound(new
                {
                    Message = "Ticket not found"
                });
            }
            return Ok(ticket);
        }
        [HttpPatch("{id:guid}/priority")]
        public async Task<IActionResult> UpdateTicketPriority(Guid id, [FromBody] UpdateTicketPriorityRequest request, CancellationToken cancellationToken)
        {
            var updatedby = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var updated = await _ticketService.UpdateTicketPriorityAsync(id, request, updatedby, cancellationToken);

            if(!updated)
            {
                return NotFound(new
                {
                    Message="Ticket not found"
                });
            }
            return NoContent();
        }
    }
}