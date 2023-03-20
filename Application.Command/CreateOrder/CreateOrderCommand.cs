

using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public CreateOrderCommand(string name,int quantity, DateTimeOffset orderDate)
        {
            Name = name;
            Quantity = quantity;
            OrderDate = orderDate;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset OrderDate { get; set; }

    }
}