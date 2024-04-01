using SeedPlusPlus.Api.Categories;
using SeedPlusPlus.Api.Orders;
using SeedPlusPlus.Api.Products;
using SeedPlusPlus.Data;

// const string filePath = @"C:\Users\lisab\Desktop\GitHub\seed-plus-plus\SeedPlusPlus.Api\bin\Debug\net7.0\Resources\categories.json";
//
// var jsonString = File.ReadAllText(filePath);
//         
// var categories = JsonSerializer.Deserialize<List<ProductCategory>>(jsonString);
//
// Console.WriteLine();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqliteDbContext(builder.Configuration.GetConnectionString("SeedsSqlite"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();

var app = builder.Build();

// Register Endpoints
app.MapProductsEndpoints();
app.MapCategoriesEndpoints();
app.MapOrdersEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();