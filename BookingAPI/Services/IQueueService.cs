using Amazon.SQS.Model;
using System.Collections.Generic;

namespace BookingAPI.Services
{
    public interface IQueueService
    {
        void ReceiveMessage(string queueUrl);
        void OnMessageReceipt();
    }
}
