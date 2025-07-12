using System;
using System.Collections.Generic;

namespace ManageProduct.DAL.Entities;

public partial class Product
{
    public int ProductID { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int CategoryID { get; set; }

    public int BrandID { get; set; }

    public string? ImageURL { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
