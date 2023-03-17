

using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;

namespace Application.Commands
{
    public class UpdateOrderCommand : IRequest<Order>
    {
        public UpdateOrderCommand(bool isConfirmed, Guid orderId)
        {

            this.isConfirmed = isConfirmed;
            this.orderId = orderId;
        }

        public bool isConfirmed { get; private set; }
        public Guid orderId { get; private set; }

    }
}