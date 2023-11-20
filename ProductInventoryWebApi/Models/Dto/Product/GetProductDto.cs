namespace ProductInventoryWebApi.Models
{
    public class GetProductDto
    {
        public int Sku { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Weight { get; set; }
    }
        
}
