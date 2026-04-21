using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblPoll
{
    public long PollId { get; set; }

    public Guid LoungeId { get; set; }

    public string? Question { get; set; }

    public DateTime? CreatedAt { get; set; }
}
