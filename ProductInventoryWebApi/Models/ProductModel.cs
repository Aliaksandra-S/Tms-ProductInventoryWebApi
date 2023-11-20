﻿namespace ProductInventoryWebApi.Models;

public class ProductModel
{
    public int Id { get; set; }

    public int Sku { get; set; }

    public string Name { get; set; } 

    public int Quantity { get; set; }

    public double Weight { get; set; }

    public int InventoryId { get; set; }

    public InventoryModel Inventory { get; set; }
}

