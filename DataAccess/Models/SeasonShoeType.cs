using System;
using System.Collections.Generic;

namespace hd_brand_asp.Models;

public partial class SeasonShoeType
{
    public int Id { get; set; }

    public int SeasonId { get; set; }
    public int ShoeTypeId { get; set; }
}
