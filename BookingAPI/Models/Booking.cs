using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Models
{
    public class Booking
    {
        public long Id { get; set; }

        public int SeatNo { get; set; }

        public int ClientId { get; set; }
    }
}
