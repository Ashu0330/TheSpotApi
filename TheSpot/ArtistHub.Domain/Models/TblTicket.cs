using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblTicket
{
    public long TicketId { get; set; }

    public long EventId { get; set; }

    public long UserId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
}
