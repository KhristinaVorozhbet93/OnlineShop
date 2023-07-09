using OnlineShop.Data;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Catalog catalog = new Catalog();

//RPC
app.MapGet("/get_products", GetProducts);
app.MapPost("/add_product", AddProduct);


//REST
app.MapGet("/products", GetProducts);
app.MapPost("/products/product", AddProduct);

app.Run();

List<Product> GetProducts()
{
    return catalog.GetProducts();
}
void AddProduct(Product product)
{
    catalog.AddProduct(product);
}