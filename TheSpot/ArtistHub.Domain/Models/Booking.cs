using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public int UserId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string? BookingTime { get; set; }

    public string? Notes { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
