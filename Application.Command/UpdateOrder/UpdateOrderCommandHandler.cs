using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.orderId);
            if (order.Quantity > order.QuantityInStock)
            {
                throw new Exception($"You have requested ({order.Quantity}) Items. But only ({order.QuantityInStock}) quantities available in the stock.We can't proceed the order.");
            }
            order.isConfirmed = request.isConfirmed;
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
            Console.WriteLine("Order has been confirmed.");
            return order;
        }
    }
}