namespace SeedPlusPlus.Core.Orders.Features;

public class GetOrderById : IUseCase<GetOrderByIdRequest, GetOrderByIdResult>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderById(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdRequest input)
    {
        var order = await _orderRepository.FindById(input.Id);

        return new GetOrderByIdResult
        {
            Order = order,
        };
    }
}

public record GetOrderByIdRequest(int Id);

public class GetOrderByIdResult : UseCaseResult
{
    public Order Order { get; set; }
}