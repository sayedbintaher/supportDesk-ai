using SupportDeskAI.Application.DTOs;

namespace SupportDeskAI.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Guid> CreateTicketAsync(CreateTicketRequest request, Guid createdBy, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TicketResponse>> GetAllTicketsAsync(CancellationToken cancellationToken = default);
        Task<TicketResponse?> GetTicketByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateTicketPriorityAsync(Guid ticketId, UpdateTicketPriorityRequest request, Guid updatedBy, CancellationToken cancellationToken = default);
    }
}