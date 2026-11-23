using SupportDeskAI.Domain.Entities;

namespace SupportDeskAI.Application.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default);
        Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default);
    }
}