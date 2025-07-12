using System;
using System.Collections.Generic;

namespace ManageProduct.DAL.Entities;

public partial class Brand
{
    public int BrandID { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
