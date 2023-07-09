using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;
using OnlineShop.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICatalog, InMemoryCatalog>();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton<IEmailSender, MailKitSmptEmailSender>();
//builder.Services.AddSingleton<IClock>(new FakeClock(new DateTime(2023, 7, 3)));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

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

List<Product> GetProducts(ICatalog catalog, IClock clock)
{
    return catalog.GetProducts(clock);
}
Product GetProductById(Guid id, ICatalog catalog, IClock clock)
{
    return catalog.GetProductById(id, clock);
}
IResult AddProduct(Product product, ICatalog catalog)
{
    catalog.AddProduct(product);
    return Results.Created($"/products/{product.Id}", product);
}
void DeleteProduct(Guid productId, ICatalog catalog)
{
    catalog.DeleteProduct(productId);
}
void UpdateProduct(Guid productId, Product newProduct, ICatalog catalog)
{
    catalog.UpdateProduct(productId, newProduct);
}
void ClearCatalog(ICatalog catalog)
{
    catalog.ClearCatalog();
}

