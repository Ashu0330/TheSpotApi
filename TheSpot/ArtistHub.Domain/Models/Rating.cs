using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public int UserId { get; set; }

    public decimal RatingValue { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}
