namespace ProductInventoryWebApi.Models;

public class InventoryModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public double TotalQuantity { get; set; } 

    public double TotalWeight { get; set; }

    public List<ProductModel> Products { get; set; }

}
