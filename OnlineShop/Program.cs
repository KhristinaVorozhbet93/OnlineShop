using OnlineShop.Data;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Catalog catalog = new Catalog();

//RPC
app.MapGet("/get_products", GetProducts);

//REST
app.MapGet("/products", GetProducts);
app.Run();

List<Product> GetProducts()
{
    return catalog.GetProducts();
}
