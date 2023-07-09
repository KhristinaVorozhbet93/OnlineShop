using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICatalog, InMemoryCatalog>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
InMemoryCatalog catalog = new InMemoryCatalog();

//RPC
app.MapGet("/get_products", GetProducts);
app.MapGet("/get_product", GetProductById);
app.MapPost("/add_product", AddProduct);
app.MapPost("/delete_product", DeleteProduct);
app.MapPost("/update_product", UpdateProduct);
app.MapPost("/clear_products", ClearCatalog);

//REST
app.MapGet("/products", GetProducts);
app.MapGet("/product", GetProductById);
app.MapPost("/products/product", AddProduct);
app.MapDelete("/products/{productId}", DeleteProduct);
app.MapPut("/products/{productId}", UpdateProduct);

app.Run();

List<Product> GetProducts()
{
    return catalog.GetProducts();
}
IResult AddProduct(Product product)
{
    catalog.AddProduct(product);
    return Results.Created($"/products/{product.Id}", product);
}
Product GetProductById(Guid id)
{
    return catalog.GetProductById(id);
}
void DeleteProduct(Guid productId)
{
    catalog.DeleteProduct(productId);
}
void UpdateProduct(Guid productId, Product newProduct)
{
    catalog.UpdateProduct(productId, newProduct);
}
void ClearCatalog()
{
    catalog.ClearCatalog();
}
