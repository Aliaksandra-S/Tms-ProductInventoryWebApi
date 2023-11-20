using Microsoft.EntityFrameworkCore;
using ProductInventoryWebApi.Data;
using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;
using ProductInventoryWebApi.Models.Dto.Inventory;
using ProductInventoryWebApi.Models.Dto.Product;

namespace ProductInventoryWebApi.Services;

public class InventoryService : IInventoryService
{
    private readonly DataContext _context;

    public InventoryService(DataContext context)
    {
        _context = context;
    }
    public async Task<CommandResultModel> AddInventory(AddInventoryDto inventory)
    {
        if (string.IsNullOrWhiteSpace(inventory.Name))
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "Name is empty",
            };
        }

        var model = new InventoryModel
        {
            Name = inventory.Name,
            TotalQuantity = 0,
            TotalWeight = 0,
            Products = new List<ProductModel>()
        };

        await _context.Inventories.AddAsync(model);
        await _context.SaveChangesAsync();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok"
        };
    }

    public async Task<CommandResultModel> DeleteInventory(DeleteInventoryDto inventory)
    {
        var inventoryModel = await _context.Inventories.SingleOrDefaultAsync(x => x.Name == inventory.Name);

        if (inventoryModel == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found"
            };
        }

        _context.Inventories.Remove(inventoryModel);
        await _context.SaveChangesAsync();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok"
        };
    }

    public async Task<InventoryListDto> GetInventories()
    {
        var inventories = await _context.Inventories.ToArrayAsync();

        return new InventoryListDto
        {
            Inventories = inventories
                .Select(x => new GetInventoryDto
                {
                    Name = x.Name,
                    TotalQuantity = x.TotalQuantity,
                    TotalWeight = x.TotalWeight,
                })
                .ToArray()
        };
    }



    public async Task<ProductListDto> GetProducts(FindInventoryDto inventory)
    {
        var inventoryModel = await _context.Inventories
            .Include(inv => inv.Products)
            .SingleOrDefaultAsync(x => x.Id == inventory.Id);
        
        if (inventoryModel != null && inventoryModel.Products != null)
        {
            return new ProductListDto
            {
                Products = inventoryModel.Products
                    .Select(x => new GetProductDto
                    {
                        Sku = x.Sku,
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Weight = x.Weight,
                    })
                    .ToArray()
            };
        }

        return null;
    }

    public async Task<CommandResultModel> AddProduct(AddProductDto product)
    {
        var inventoryModel = await _context.Inventories.FindAsync(product.InventoryId);

        if (inventoryModel == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found",
            };
        }

        var productModel = new ProductModel
        {
            Sku = product.Sku,
            Name = product.Name,
            Quantity = product.Quantity,
            Weight = product.Weight,
            InventoryId = inventoryModel.Id,
            Inventory = inventoryModel,
        };

        await _context.Products.AddAsync(productModel);

        inventoryModel.Products.Add(productModel);
        inventoryModel.TotalQuantity += product.Quantity;
        inventoryModel.TotalWeight += product.Weight;

        await _context.SaveChangesAsync();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok",
        };
    }

    public async Task<CommandResultModel> DeleteProduct(DeleteProductDto productToDelete)
    {
        var inventoryModel = await _context.Inventories.FindAsync(productToDelete.InventoryId);

        if (inventoryModel == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found",
            };
        }

        var product = await _context.Products.FindAsync(productToDelete.ProductId);

        if (product == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "product not found",
            };
        }

        _context.Products.Remove(product);

        inventoryModel.TotalWeight -= product.Weight;

        await _context.SaveChangesAsync();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok",
        };
    }

    public async Task<CommandResultModel> EditProduct(EditProductDto productToEdit)
    {
        var inventoryModel = await _context.Inventories.FindAsync(productToEdit.InventoryId);

        if (inventoryModel == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found",
            };
        }

        var product = await _context.Products.FindAsync(productToEdit.ProductId);

        if (product == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "product not found",
            };
        }

        inventoryModel.TotalWeight -= product.Weight - productToEdit.Weight;
        inventoryModel.TotalQuantity -= product.Quantity - productToEdit.Quantity;

        product.Name = productToEdit.Name;
        product.Quantity = productToEdit.Quantity;
        product.Weight = productToEdit.Weight;

        await _context.SaveChangesAsync();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok",
        };
    }

    public InventoryModel? FindInventory(string inventoryName)
    {
        return _context.Inventories.Where(x => x.Name == inventoryName).FirstOrDefault();
    }
}

