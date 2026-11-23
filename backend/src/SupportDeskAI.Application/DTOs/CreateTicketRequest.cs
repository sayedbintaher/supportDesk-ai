using System.ComponentModel.DataAnnotations;

namespace SupportDeskAI.Application.DTOs
{
    public class CreateTicketRequest
    {
        [Required]
        [StringLength(200, MinimumLength=1)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(4000, MinimumLength=1)]
        public string Description { get; set; } = string.Empty;
    }
}