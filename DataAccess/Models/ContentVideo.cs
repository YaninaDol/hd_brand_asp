using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class ContentVideo
{
    public int Id { get; set; }

    public string URL { get; set; } = null!;

    public int prodId { get; set; } 
}
