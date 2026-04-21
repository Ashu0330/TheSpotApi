using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblLounge
{
    public long LoungeId { get; set; }

    public long UserId { get; set; }

    public string? LoungeName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public int? Capacity { get; set; }

    public string? Description { get; set; }

    public decimal? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsVerified { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsActive { get; set; }
}
