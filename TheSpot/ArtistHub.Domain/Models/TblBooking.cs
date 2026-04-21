using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblBooking
{
    public long BookingId { get; set; }

    public int? EventId { get; set; }

    public long ArtistId { get; set; }

    public int? LoungeId { get; set; }

    public DateOnly BookingDate { get; set; }

    public decimal? Amount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? BookedByUserId { get; set; }

    public string? BookingType { get; set; }
}
