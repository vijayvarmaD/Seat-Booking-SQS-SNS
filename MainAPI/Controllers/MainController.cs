using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using MainAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainAPI.Controllers
{
    [Route("api/main")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public MainController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public ActionResult Book(int seatNo, int clientId)
        {
            // form message
            Dictionary<string, MessageAttributeValue> msgAttrs = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "SeatNo", new MessageAttributeValue
                    {
                        DataType = "Number", StringValue = seatNo.ToString()
                    }
                },
                {
                    "ClientId", new MessageAttributeValue
                    {
                        DataType = "Number", StringValue = clientId.ToString()
                    }
                }
            };

            string msgBody = "Ticket Booking";
            string queueUrl = "https://sqs.ap-south-1.amazonaws.com/280786725273/Booking.fifo";

            // Send message to Booking Queue
            var res = _queueService.SendMessage(msgBody, msgAttrs, queueUrl);

            return Ok(res);
        }
    }
}