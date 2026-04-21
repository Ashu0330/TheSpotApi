using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblArtistMedium
{
    public int ArtistMediaId { get; set; }

    public int ArtistId { get; set; }

    public string? MediaCategory { get; set; }

    public string? FileUrl { get; set; }

    public string? Title { get; set; }

    public int? DisplayOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }
}
