using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblLoungeMedium
{
    public long MediaId { get; set; }

    public long LoungeId { get; set; }

    public string? MediaUrl { get; set; }

    public string? MediaType { get; set; }

    public DateTime? CreatedAt { get; set; }
}
