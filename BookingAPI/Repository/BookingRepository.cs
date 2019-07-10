using BookingAPI.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IConfiguration _config;

        public BookingRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }

        public async Task<int> BookTicket(int seatNo, int clientId)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = $"UPDATE Booking SET ClientId = {clientId} WHERE SeatNo = {seatNo}";
                var result = await conn.ExecuteAsync(sQuery);
                return result;
            }
        }

        public async Task<Booking> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT SeatNo, ClientId FROM Booking WHERE Id = @ID";
                var result = await conn.QueryAsync<Booking>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Booking>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT SeatNo, ClientId FROM Booking";
                var result = await conn.QueryAsync<Booking>(sQuery);
                return result.ToList();
            }
        }
    }
}
