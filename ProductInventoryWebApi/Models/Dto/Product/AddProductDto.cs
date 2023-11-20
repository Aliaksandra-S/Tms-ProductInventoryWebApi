namespace ProductInventoryWebApi.Models
{
    public class AddProductDto
    {
        public int InventoryId { get; set; }

        public int Sku { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }
    }
        
}
