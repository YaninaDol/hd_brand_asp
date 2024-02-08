﻿using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class Season
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
