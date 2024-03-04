using Microsoft.AspNetCore.Mvc;
using SeedPlusPlus.Core.Orders;
using SeedPlusPlus.Core.Orders.Features;

namespace SeedPlusPlus.Api.Endpoints;

public static class Orders
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet("/orders/{id:int}", GetOrderByIdAsync)
            .WithName("GetOrder");
        
        routeBuilder
            .MapPost("/orders", CreateOrderAsync)
            .WithName("CreateOrder");
        
        routeBuilder
            .MapPut("/orders/{id:int}", UpdateOrderAsync)
            .WithName("UpdateOrder");
        
        routeBuilder
            .MapDelete("/orders/{id:int}", RemoveOrderByIdAsync)
            .WithName("DeleteOrder");
        
        return routeBuilder;
    }

    private static async Task<ActionResult<string>> GetOrderByIdAsync(int id, IOrderRepository orderRepository)
    {
        var handler = new GetOrderById(orderRepository);
        var response = await handler.Handle(new GetOrderByIdRequest(id));
        
        // TODO
        return $"Order with id {id}";
    }

    private static async Task<ActionResult<string>> UpdateOrderAsync(int id)
    {
        return $"Updating product with ID: {id}";
    }

    private static async Task RemoveOrderByIdAsync(int id)
    {
        Console.WriteLine($"Order with id {id} was removed");
    }

    private static async Task<ActionResult<string>> CreateOrderAsync()
    {
        return "Order created";
    }
}