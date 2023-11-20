namespace ProductInventoryWebApi.Models
{
    public class GetInventoryDto
    {
        public string Name { get; set; }

        public double TotalQuantity { get; set; } 

        public double TotalWeight { get; set; }
    }
}
