using Microsoft.EntityFrameworkCore;
using ProductInventoryWebApi.Data;
using ProductInventoryWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var contextOptions = new DbContextOptionsBuilder<DataContext>()
    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

using var context = new DataContext(contextOptions.Options);

builder.Services.AddSingleton<IInventoryService>(new InventoryService(context));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
