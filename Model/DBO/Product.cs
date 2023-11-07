using System;
using System.Collections.Generic;

namespace Demo_var_6;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ArticleNumber { get; set; }

    public string? Name { get; set; }

    public string? Unit { get; set; }

    public decimal? Cost { get; set; }

    public int? MaximumDiscountAmount { get; set; }

    public string? Manufacturer { get; set; }

    public string? Provider { get; set; }

    public int? Category { get; set; }

    public int? CurrentDiscount { get; set; }

    public int? QuantityInStock { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual ProductCategory? CategoryNavigation { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
