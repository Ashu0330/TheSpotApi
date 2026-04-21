using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Domain
{
    public class LoungeDto
    {
        public int LoungeId { get; set; }
        public int UserId { get; set; }

        public string? LoungeName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? Capacity { get; set; }
        public string? Description { get; set; }
        public decimal? Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? MediaUrl { get; set; }
        public string? MediaType { get; set; }

        public int TotalCount { get; set; }

    }
}
