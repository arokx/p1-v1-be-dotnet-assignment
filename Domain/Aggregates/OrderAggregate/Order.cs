using Domain.Exceptions;
using Domain.SeedWork;
using System;
using System.Runtime.CompilerServices;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(string name,int quantity,int quantityInStock, DateTimeOffset orderDate, DateTimeOffset? orderConfirmedDate = null, bool isConfirmed = false) : this()
        {
            Name = name;
            Quantity = quantity;
            QuantityInStock = quantityInStock;
            OrderDate = orderDate;
            if (isConfirmed)
                OrderConfirmedDate = DateTimeOffset.Now;
            else
                OrderConfirmedDate = orderConfirmedDate;
            this.isConfirmed = isConfirmed;
        }
        public Order()
        {
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int QuantityInStock { get; set; }
        public DateTimeOffset OrderDate { get;set; }
        public DateTimeOffset? OrderConfirmedDate { get;set; }
        public bool isConfirmed { get; set; }

    }
}