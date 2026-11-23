using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportDeskAI.Domain.Entities;

namespace SupportDeskAI.Infrastructure.Persistence
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(ticket => ticket.Id);

            builder.Property(ticket => ticket.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ticket => ticket.Description)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(ticket => ticket.Status)
                .IsRequired();

            builder.Property(ticket => ticket.Priority)
                .IsRequired();

            builder.Property(ticket => ticket.CreatedAt)
                .IsRequired();

            builder.Property(ticket => ticket.CreatedBy)
                .IsRequired();

            builder.Property(ticket => ticket.UpdatedAt)
                .IsRequired(false);

            builder.Property(ticket => ticket.UpdatedBy)
                .IsRequired(false);
        }
    }
}