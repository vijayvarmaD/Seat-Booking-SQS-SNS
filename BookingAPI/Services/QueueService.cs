using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookingAPI.Services
{
    public class QueueService : IQueueService
    {
        public IAmazonSQS sqs;

        public QueueService()
        {
            this.sqs = new AmazonSQSClient(RegionEndpoint.APSouth1);
        }

        public async void ReceiveMessage(string queueUrl)
        {
            var receiveReq = new SetQueueAttributesRequest
            {
                Attributes = new Dictionary<string, string>
                {
                    { "ReceiveMessageWaitTimeSeconds", "20"}
                },
                QueueUrl = queueUrl
            };
            var res = await sqs.SetQueueAttributesAsync(receiveReq);

            
        }

        public async void OnMessageReceipt()
        {
            var recieveTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var req = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = "https://sqs.ap-south-1.amazonaws.com/280786725273/Booking.fifo",
                WaitTimeSeconds = 20
            };

            var res = await sqs.ReceiveMessageAsync(req, recieveTimeout.Token);
        }
    }
}
