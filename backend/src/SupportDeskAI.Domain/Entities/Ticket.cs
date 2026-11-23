using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupportDeskAI.Domain.Enums;

namespace SupportDeskAI.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketPriority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        private Ticket()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public Ticket(string title, string description, Guid createdBy)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Ticket title cannot be empty.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Ticket description cannot be empty.", nameof(description));
            }

            if (createdBy == Guid.Empty)
            {
                throw new ArgumentException("CreatedBy must be a valid user id.");
            }

            Id = Guid.NewGuid();
            Title = title.Trim();
            Description = description.Trim();
            Status = TicketStatus.Open;
            Priority = TicketPriority.Medium;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void ChangePriority(TicketPriority priority, Guid updatedBy)
        {
            ValidateUpdatedBy(updatedBy);

            if (!Enum.IsDefined(priority))
            {
                throw new ArgumentOutOfRangeException(nameof(priority));
            }

            Priority = priority;
            SetAuditFields(updatedBy);
        }

        public void StartProgress(Guid updatedBy)
        {
            ValidateUpdatedBy(updatedBy);

            if (Status != TicketStatus.Open)
            {
                throw new InvalidOperationException("Only open tickets can be moved to In Progress.");
            }
            Status = TicketStatus.InProgress;
            SetAuditFields(updatedBy);
        }
        public void Resolve(Guid updatedBy)
        {
            ValidateUpdatedBy(updatedBy);

            if (Status != TicketStatus.InProgress)
            {
                throw new InvalidOperationException("Only tickets in progress can be resolved.");
            }

            Status = TicketStatus.Resolved;
            SetAuditFields(updatedBy);
        }
        public void Close(Guid updatedBy)
        {
            ValidateUpdatedBy(updatedBy);

            if (Status != TicketStatus.Resolved)
                throw new InvalidOperationException(
                    "Only resolved tickets can be closed.");

            Status = TicketStatus.Closed;
            SetAuditFields(updatedBy);
        }
        private static void ValidateUpdatedBy(Guid updatedBy)
        {
            if (updatedBy == Guid.Empty)
            {
                throw new ArgumentException(
                    "UpdatedBy must be a valid user ID.",
                    nameof(updatedBy));
            }

        }

        private void SetAuditFields(Guid updatedBy)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
}