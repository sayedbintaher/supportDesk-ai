using SupportDeskAI.Application.DTOs;
using SupportDeskAI.Application.Interfaces;
using SupportDeskAI.Domain.Entities;
using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Application.Services
{
    public class TicketService : ITicketService
    {
        public readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Guid> CreateTicketAsync(CreateTicketRequest request, Guid createdBy, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var ticket = new Ticket(request.Title, request.Description, createdBy);

            await _ticketRepository.AddAsync(ticket, cancellationToken);
            return ticket.Id;
        }

        public async Task<IReadOnlyList<TicketResponse>> GetAllTicketsAsync(CancellationToken cancellationToken = default)
        {
            var tickets = await _ticketRepository.GetAllAsync(cancellationToken);
            return tickets.Select(ticket => new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                CreatedAt = ticket.CreatedAt,
                CreatedBy = ticket.CreatedBy,
                UpdatedAt = ticket.UpdatedAt,
                UpdatedBy = ticket.UpdatedBy
            }).ToList();
        }

        public async Task<TicketResponse?> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _ticketRepository.GetByIdAsync(
                id, cancellationToken);

            if (ticket is null)
            {
                return null;
            }

            return new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                CreatedAt = ticket.CreatedAt,
                CreatedBy = ticket.CreatedBy,
                UpdatedAt = ticket.UpdatedAt,
                UpdatedBy = ticket.UpdatedBy
            };
        }
        public async Task<bool> UpdateTicketPriorityAsync(Guid ticketId, UpdateTicketPriorityRequest request, Guid updatedBy, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var ticket = await _ticketRepository.GetByIdAsync(ticketId, cancellationToken);

            if(ticket is null)
            {
                return false;
            }

            ticket.ChangePriority(request.Priority!.Value, updatedBy);

            await _ticketRepository.UpdateAsync(ticket, cancellationToken);
            return true;
        }
        public async Task<bool> UpdateTicketStatusAsync(Guid ticketId, UpdateTicketStatusRequest request, Guid updatedBy, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var ticket = await _ticketRepository.GetByIdAsync(ticketId, cancellationToken);

            if (ticket is null)
            {
                return false;
            }
            switch(request.Status)
            {
                case TicketStatus.InProgress:
                    ticket.StartProgress(updatedBy);
                    break;
                
                case TicketStatus.Resolved:
                    ticket.Resolve(updatedBy);
                    break;
                
                case TicketStatus.Closed:
                    ticket.Close(updatedBy);
                    break;
                
                default:
                    throw new ArgumentException("Invalid or unsupported ticket status.");          
            }

            await _ticketRepository.UpdateAsync(ticket, cancellationToken);

            return true;
        }
    }
}