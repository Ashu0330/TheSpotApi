using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblArtist
{
    public long ArtistId { get; set; }

    public long UserId { get; set; }

    public int? CategoryId { get; set; }

    public decimal? PricePerShow { get; set; }

    public string? Bio { get; set; }

    public decimal? Rating { get; set; }

    public int? TotalShows { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsActive { get; set; }

    public string? Spotify { get; set; }

    public string? YouTube { get; set; }

    public string? Instagram { get; set; }
}
