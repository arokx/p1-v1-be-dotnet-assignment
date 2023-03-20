using System.Threading;
using System.Threading.Tasks;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Aggregates.QuantityAggregate;
using MediatR;

namespace Application.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IQuantityRepository _quantityRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IQuantityRepository quantityRepository) 
        {
            _orderRepository = orderRepository;
            _quantityRepository = quantityRepository;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.orderId);
            var quantityInStock = await _quantityRepository.GetAsync();
            if (order.Quantity > quantityInStock[0].AvailableQuantity)
            {
                throw new Exception($"You have requested ({order.Quantity}) Items. But only ({quantityInStock[0].AvailableQuantity}) quantities available in the stock.We can't proceed the order.");
            }
            if (order.isConfirmed) {
                throw new Exception("Order is already confirmed.");
            }
            if (request.isConfirmed)
            {
                order.OrderConfirmedDate= DateTime.UtcNow;
                order.isConfirmed = true;
            }
            _orderRepository.Update(order);
            var quantity = quantityInStock[0];
            quantity.AvailableQuantity = quantityInStock[0].AvailableQuantity - order.Quantity;
            _quantityRepository.Update(quantity);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
            Console.WriteLine("Order has been confirmed.");
            return order;
        }
    }
}