using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class ArtistAvailability
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public DateOnly AvailableDate { get; set; }

    public string Status { get; set; } = null!;
}
