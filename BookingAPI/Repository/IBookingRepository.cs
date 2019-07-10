using BookingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> GetByID(int id);

        Task<List<Booking>> GetAll();

        Task<int> BookTicket(int seatNo, int clientId);
    }
}
