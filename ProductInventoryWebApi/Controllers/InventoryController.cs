using Microsoft.AspNetCore.Mvc;
using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;
using ProductInventoryWebApi.Models.Dto.Inventory;
using ProductInventoryWebApi.Models.Dto.Product;
using ProductInventoryWebApi.Services;

namespace ProductInventoryWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<InventoryListDto> GetInventories()
    {
        var result = await _service.GetInventories();

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> AddInventory([FromBody] AddInventoryDto inventory)
    {
        var result = await _service.AddInventory(inventory);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteInventory([FromBody] DeleteInventoryDto inventory)
    {
        var result = await _service.DeleteInventory(inventory);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ProductListDto> GetProducts([FromBody] FindInventoryDto inventory)
    {
        var products = await _service.GetProducts(inventory);

        return products;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
    {
       var result = await _service.AddProduct(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task <IActionResult> DeleteProduct([FromBody] DeleteProductDto product)
    {
        var result = await _service.DeleteProduct(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct([FromBody] EditProductDto product)
    {
        var result = await _service.EditProduct(product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
