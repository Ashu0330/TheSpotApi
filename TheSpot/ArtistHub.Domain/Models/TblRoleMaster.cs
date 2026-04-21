using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class TblRoleMaster
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
