using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;

namespace ProductInventoryWebApi.Services;

public interface IInventoryService
{
    InventoryListDto GetInventories();

    CommandResultModel AddInventory(AddInventoryDto inventory);

    ProductListDto GetProducts(string inventoryName);

    CommandResultModel AddProduct(string inventoryName, ProductDto product);

    CommandResultModel DeleteProduct(string inventoryName, DeleteProductDto product);

}

