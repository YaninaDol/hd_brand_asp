using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class Productsize
{
    public int Productid { get; set; }

    public int Sizeid { get; set; }

    public virtual Product Product { get; set; } = null!;
}
