using Microsoft.EntityFrameworkCore;
using ProductInventoryWebApi.Models;

namespace ProductInventoryWebApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<ProductModel> Products { get; set; }

    public DbSet<InventoryModel> Inventories { get; set; }
}

