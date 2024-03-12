using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class LikedProduct
{
    public int Id { get; set; }

    public string? UserId { get; set; }
    public int? ProductId { get; set;}
}
