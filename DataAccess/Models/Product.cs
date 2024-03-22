using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class Product
{
    public int Id { get; set; }
    public string Article { get; set; }
    public string? Image3 { get; set; }
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string? Image2 { get; set; } 
    public string? Video { get; set; } 
    public bool? isNew { get; set; }
    public bool? isDiscount { get; set; }
    public bool WeeklyLook { get; set; }
    public string? SubCategoryid { get; set; }

    public int? Categoryid { get; set; }

    public int? Seasonid { get; set; }
    public int? Materialid { get; set; }
    
    public int? Price { get; set; }
    public int? SalePrice { get; set; }
    public string? Sizes { get; set; }
    public string? Color { get; set; }
    public virtual Category? Category { get; set; }

    public virtual ICollection<Productssize> ProductSizes { get; set; } = new List<Productssize>();

    public virtual Season? Season { get; set; }
}
