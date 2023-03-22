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
            // Get the Order object to be updated from the repository.
            var order = await _orderRepository.GetAsync(request.orderId);
            // Get the available quantity from Quantity table
            var quantityInStock = await _quantityRepository.GetAsync();
            // Check if the requested quantity is greater than the available quantity.
            if (order.Quantity > quantityInStock[0].AvailableQuantity)
            {
                throw new Exception($"You have requested ({order.Quantity}) Items. But only ({quantityInStock[0].AvailableQuantity}) quantities available in the stock.We can't proceed the order.");
            }
            // Check if the order has already been confirmed.
            if (order.isConfirmed) {
                throw new Exception("Order is already confirmed.");
            }
            // If the request indicates that the order should be confirmed, update the Order object accordingly.
            if (request.isConfirmed)
            {
                order.OrderConfirmedDate= DateTime.UtcNow;
                order.isConfirmed = true;
            }
            _orderRepository.Update(order);

            // Update the available quantity in the Quantity table in the repository.
            var quantity = quantityInStock[0];          
            quantity.AvailableQuantity = quantityInStock[0].AvailableQuantity - order.Quantity;
            _quantityRepository.Update(quantity);

            await _orderRepository.UnitOfWork.SaveEntitiesAsync();
            Console.WriteLine("Order has been confirmed.");
            return order;
        }
    }
}