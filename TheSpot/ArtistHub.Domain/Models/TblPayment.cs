using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblPayment
{
    public long PaymentId { get; set; }

    public long UserId { get; set; }

    public long? ReferenceId { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentType { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Gateway { get; set; }

    public DateTime? CreatedAt { get; set; }
}
