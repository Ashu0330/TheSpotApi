using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistHub.Presentation.Domain
{
    public class ArtistDashboardDto
    {
        public string? UserName { get; set; }

        public int PendingBookings { get; set; }

        public int UpcomingShows { get; set; }

        public decimal AverageRating { get; set; }

        public int ApprovedBookingsThisMonth { get; set; }

        public decimal LifetimeEarnings { get; set; }

        public decimal MonthlyEarnings { get; set; }

        //public string? RecentRequests { get; set; }

        //public string? PendingRequests { get; set; }

        public List<RecentRequest>? RecentRequestlist { get; set; }

        public List<PendingRequest>? PendingRequestlist { get; set; }
        public string? RecentRequests { get; set; }

        public string? PendingRequests { get; set; }
        public string? TotalReviewMembers { get; set; }

    }
    public class RecentRequest
    {
        public int BookingId { get; set; }

        public string? FullName { get; set; }

        public int EventId { get; set; }

        public int LoungeId { get; set; }

        public DateTime BookingDate { get; set; }

        public decimal Amount { get; set; }

        public string? Status { get; set; }
        public string? BookedBy { get; set; }
        public string? Address { get; set; }

    }
    public class PendingRequest
    {
        public int BookingId { get; set; }

        public int EventId { get; set; }

        public int Venue { get; set; }

        public DateTime BookingDate { get; set; }

        public decimal Rate { get; set; }

        public string? Status { get; set; }
    }
}
