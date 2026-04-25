using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblEvent
{
        public long EventId { get; set; }

        public long LoungeId { get; set; }

        public long? ArtistId { get; set; }

        public string? Title { get; set; }

        public DateOnly EventDate { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public decimal? TicketPrice { get; set; }

        public int? TotalSeats { get; set; }

        public int? AvailableSeats { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsDeleted { get; set; }
}
