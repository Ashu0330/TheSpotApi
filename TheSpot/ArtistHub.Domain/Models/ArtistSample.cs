using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class ArtistSample
{
    public int Id { get; set; }

    public int ArtistId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
