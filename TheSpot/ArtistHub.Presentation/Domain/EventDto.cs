using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Domain
{
    public class EventDto
    {
        public int EventId { get; set; }
        public int LoungeId { get; set; }
        public int ArtistId { get; set; }

        public string? Title { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public decimal? TicketPrice { get; set; }
        public int? TotalSeats { get; set; }
        public int? AvailableSeats { get; set; }

        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? LoungeName { get; set; }
        public string? City { get; set; }
        public int? CategoryId { get; set; }

        public int TotalCount { get; set; }
    }
}
