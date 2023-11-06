namespace ProductInventoryWebApi.Models
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public double Weight { get; set; } = 0;
    }
}
