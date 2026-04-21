using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class CategoryMaster
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }
}
