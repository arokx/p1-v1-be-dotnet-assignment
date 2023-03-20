using Domain.Aggregates.OrderAggregate;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.QuantityAggregate
{
    public interface IQuantityRepository : IRepository<Quantity>
    {
        void Update(Quantity quantity);

        Task<List<Quantity>> GetAsync();
    }
}
