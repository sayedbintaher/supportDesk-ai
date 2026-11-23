using Microsoft.EntityFrameworkCore;
using SupportDeskAI.Domain.Entities;

namespace SupportDeskAI.Infrastructure.Persistence
{
    public class SupportDeskDbContext : DbContext
    {
        public SupportDeskDbContext(DbContextOptions<SupportDeskDbContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(
        ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SupportDeskDbContext).Assembly);
        }
    }
}