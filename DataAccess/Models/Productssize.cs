using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hd_brand_asp.Models;

public partial class Productssize
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Productid { get; set; }
    public string Size { get; set; }

   
    public string Image { get; set; }
    public string Name { get; set; }
    public int? Price { get; set; }

    public virtual Product Product { get; set; } = null!;
}