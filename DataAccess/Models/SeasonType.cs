using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class SeasonType
{
    public int Id { get; set; }

    public int SeasonId { get; set; }
    public int SubCategoryId { get; set; }
}
