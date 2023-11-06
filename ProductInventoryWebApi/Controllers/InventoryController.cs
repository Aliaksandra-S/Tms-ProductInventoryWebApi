using Microsoft.AspNetCore.Mvc;
using ProductInventoryWebApi.Models;
using ProductInventoryWebApi.Models.Dto;
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
    public InventoryListDto GetInventories()
    {
        var result = _service.GetInventories();

        return result;
    }

    [HttpPost]
    public IActionResult AddInventory([FromBody] AddInventoryDto inventory)
    {
        var result = _service.AddInventory(inventory);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("{inventoryName}")]
    public ProductListDto GetProducts([FromRoute] string inventoryName)
    {
        var products = _service.GetProducts(inventoryName);

        if (products == null)
        {
            BadRequest();
        }

        return products;
    }

    [HttpPost("{inventoryName}")]
    public IActionResult AddProduct([FromRoute] string inventoryName, [FromBody] ProductDto product)
    {
       var result = _service.AddProduct(inventoryName, product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("{inventoryName}")]
    public IActionResult DeleteProduct([FromRoute] string inventoryName, [FromBody] DeleteProductDto product)
    {
        var result = _service.DeleteProduct(inventoryName, product);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
