﻿using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class Productsize
{
    public int Productid { get; set; }
    public string Size { get; set; }

   
    public string Image { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public virtual Product Product { get; set; } = null!;
}