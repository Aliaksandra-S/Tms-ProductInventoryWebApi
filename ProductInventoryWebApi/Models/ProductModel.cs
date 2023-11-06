namespace ProductInventoryWebApi.Models;

public class ProductModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int InventoryId { get; set; } = 0;

    public InventoryModel? Inventory { get; set; }

    public int Quantity { get; set; } = 0;

    public double Weight { get; set; } = 0;
}

