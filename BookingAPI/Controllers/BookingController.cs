using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
using BookingAPI.Repository;
using BookingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IQueueService _queueService;

        public BookingController(IBookingRepository bookingRepository, IQueueService queueService)
        {
            _bookingRepository = bookingRepository;
            _queueService = queueService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Booking>> GetByID(int id)
        {
            return await _bookingRepository.GetByID(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Booking>>> GetAll()
        {
            return await _bookingRepository.GetAll();
        }

        [HttpPost]
        [Route("book")]
        public async Task<ActionResult> BookTicket(int seatNo, int clientId)
        {
            await _bookingRepository.BookTicket(seatNo, clientId);
            return Ok();
        }

        [HttpGet]
        [Route("pollQueue")]
        public async Task<ActionResult> PollQueue()
        {
            _queueService.OnMessageReceipt();
            return Ok();
        }
    }
}