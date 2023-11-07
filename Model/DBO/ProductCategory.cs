using System;
using System.Collections.Generic;

namespace Demo_var_6;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
