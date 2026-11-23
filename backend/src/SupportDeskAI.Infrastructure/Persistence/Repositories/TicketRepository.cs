using Microsoft.EntityFrameworkCore;
using SupportDeskAI.Application.Interfaces;
using SupportDeskAI.Domain.Entities;

namespace SupportDeskAI.Infrastructure.Persistence.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SupportDeskDbContext _context;

        public TicketRepository(SupportDeskDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets.FirstOrDefaultAsync(
                ticket => ticket.Id == id, cancellationToken
            );
        }

        public async Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tickets.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken cancellationToken = default)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}