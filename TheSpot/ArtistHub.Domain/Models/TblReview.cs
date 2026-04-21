using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblReview
{
    public long ReviewId { get; set; }

    public long UserId { get; set; }

    public long TargetId { get; set; }

    public string? TargetType { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }
}
