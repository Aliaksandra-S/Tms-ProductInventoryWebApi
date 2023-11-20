namespace ProductInventoryWebApi.Models.Dto.Product;

public class EditProductDto
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int Sku { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public double Weight { get; set; }
}

