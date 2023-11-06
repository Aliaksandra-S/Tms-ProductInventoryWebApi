using ProductInventoryWebApi.Data;
using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;

namespace ProductInventoryWebApi.Services;

public class InventoryService : IInventoryService
{
    private readonly DataContext _context;

    public InventoryService(DataContext context)
    {
        _context = context;
    }
    public CommandResultModel AddInventory(AddInventoryDto inventory)
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
        };

        _context.Inventories.Add(model);
        _context.SaveChanges();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok"
        };
    }

    public InventoryListDto GetInventories()
    {
        return new InventoryListDto
        {
            Inventories = _context.Inventories
                .Select(x => new GetInventoryDto
                {
                    Name = x.Name,
                    TotalQuantity = x.TotalQuantity,
                    TotalWeight = x.TotalWeight,
                })
                .ToArray()
        };
    }

    public ProductListDto GetProducts(string inventoryName)
    {
        var inventory = FindInventory(inventoryName);

        if (inventory != null)
        {
            return new ProductListDto
            {
                Products = _context.Products
                    .Where(inv => inv.InventoryId == inventory.Id)
                    .Select(x => new ProductDto
                    {
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Weight = x.Weight,
                    })
                    .ToArray()
            };
        }

        return null;
    }

    public CommandResultModel AddProduct(string inventoryName, ProductDto product)
    {
        var inventory = FindInventory(inventoryName);

        if (inventory == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found",
            };
        }

        _context.Products.Add(new ProductModel
        {
            Name = product.Name,
            Quantity = product.Quantity,
            Weight = product.Weight,
            InventoryId = inventory.Id,
            Inventory = inventory,
        });

        inventory.TotalQuantity += product.Quantity;
        inventory.TotalWeight += product.Weight;

        _context.SaveChanges();

        return new CommandResultModel
        {
            Success = true,
            Message = "ok",
        };
    }

    public CommandResultModel DeleteProduct(string inventoryName, DeleteProductDto productToDelete)
    {
        var inventory = FindInventory(inventoryName);

        if (inventory == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "inventory not found",
            };
        }

        var product = _context.Products.FirstOrDefault(x => x.Name == productToDelete.Name);

        if (product == null)
        {
            return new CommandResultModel
            {
                Success = false,
                Message = "product not found",
            };
        }

        if (product.Quantity > productToDelete.Quantity)
        {
            product.Quantity -= productToDelete.Quantity;
        }
        else
        {
            _context.Products.Remove(product);
        }

        inventory.TotalWeight -= product.Weight;
        inventory.TotalQuantity -= productToDelete.Quantity;

        _context.SaveChanges();

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

