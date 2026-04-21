using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ArtistHub.Presentation.Domain
{
    public class PaginationModel
    {
        public string? SortBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class filterModel
    {
        public int? ArtistId { get; set; }
        public int? LoungeId { get; set; }
        public int? EventId { get; set; }
        public string? Status { get; set; }

    }
    public class ArtistExploreFilterModel : PaginationModel
    {
        public string? Flag { get; set; }
        public int? CategoryId { get; set; }
        public string? Status { get; set; }
        public decimal? PricePerShow { get; set; }
        public string? City { get; set; }
        public decimal? Rating { get; set; }
        public int? TotalShows { get; set; }
        public string? BookingDate { get; set; }

        public class EventExploreFilterModel : PaginationModel
        {
            public string? Flag { get; set; }
            public int? ArtistId { get; set; }
            public int? LoungeId { get; set; }
            public DateTime? EventFromDate { get; set; }
            public DateTime? EventToDate { get; set; }
            public decimal? MinTicketPrice { get; set; }
            public decimal? MaxTicketPrice { get; set; }
            public string? EventStatus { get; set; }
            public string? City { get; set; }
            public int? CategoryId { get; set; }
        }
        public class LoungeExploreFilterModel : PaginationModel
        {
            public string? Flag { get; set; } 
            public string? LoungeName { get; set; }
            public int? Capacity { get; set; }
            public string? City { get; set; }
            public decimal? Rating { get; set; }
        }
    }
}
