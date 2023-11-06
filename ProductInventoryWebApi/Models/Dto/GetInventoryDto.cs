namespace ProductInventoryWebApi.Models
{
    public class GetInventoryDto
    {
        public string Name { get; set; }

        public double TotalQuantity { get; set; } = 0;

        public double TotalWeight { get; set; } = 0;
    }
}
