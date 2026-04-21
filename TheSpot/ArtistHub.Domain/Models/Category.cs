using System;
using System.Collections.Generic;

namespace ArtistHub.Domain.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ArtistCount { get; set; }
}
