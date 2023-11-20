using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;
using ProductInventoryWebApi.Models.Dto.Inventory;
using ProductInventoryWebApi.Models.Dto.Product;

namespace ProductInventoryWebApi.Services;

public interface IInventoryService
{
    Task<CommandResultModel> AddInventory(AddInventoryDto inventory);

    Task<CommandResultModel> DeleteInventory(DeleteInventoryDto inventory);

    Task<InventoryListDto> GetInventories();

    Task<ProductListDto> GetProducts(FindInventoryDto inventory);

    Task<CommandResultModel> AddProduct(AddProductDto product);

    Task<CommandResultModel> DeleteProduct(DeleteProductDto product);

    Task<CommandResultModel> EditProduct(EditProductDto productToEdit);

}

