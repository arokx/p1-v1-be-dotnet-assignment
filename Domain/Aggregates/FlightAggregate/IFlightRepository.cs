using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRepository
    {
        Flight Add(Flight flight);

        void Update(Flight flight);

        Task<Flight> GetAsync(Guid flightId);

        Task<List<Flight>> GetAllAsync();

        Task<List<AvailableFlightsViewModel>> GetAllAvailableFlightsAsync(string code);
    }
}