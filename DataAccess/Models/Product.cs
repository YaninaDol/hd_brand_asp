using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ShoeType { get; set; }

    public int? Categoryid { get; set; }

    public int? Seasonid { get; set; }
    public int? Materialid { get; set; }
    
    public int? Price { get; set; }
    public virtual Category? Category { get; set; }

    public virtual ICollection<Productsize> Productsizes { get; } = new List<Productsize>();

    public virtual Season? Season { get; set; }
}
