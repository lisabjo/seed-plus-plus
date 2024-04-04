using SeedPlusPlus.Api;
using SeedPlusPlus.Api.Categories;
using SeedPlusPlus.Api.Products;
using SeedPlusPlus.Api.Tags;
using SeedPlusPlus.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqliteDbContext(builder.Configuration.GetConnectionString("SeedsSqlite"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.RegisterHandlers();

var app = builder.Build();

// Register Endpoints
app.MapProductsEndpoints();
app.MapCategoriesEndpoints();
app.MapTagsEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();